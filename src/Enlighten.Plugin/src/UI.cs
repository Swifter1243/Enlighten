using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Beatmap.Base;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin
{
	public class UI
	{
		public Enlighten plugin;
		public GameObject panelAsset;
		public Transform canvas;
		public EnlightenPanel panel;

		public UI(Enlighten plugin)
		{
			this.plugin = plugin;

			// Extension Button
			var button = new ExtensionButton();
			button.Tooltip = "Enlighten";
			button.Click = OnPress;
			button.Icon = plugin.bundle.LoadAsset<Sprite>("Assets/Images/icon.png");
			ExtensionButtons.AddButton(button);

			// UI Setup
			panelAsset = plugin.bundle.LoadAsset<GameObject>("Assets/EnlightenPanel.prefab");
			var enlightenPanel = panelAsset.AddComponent<EnlightenPanel>();

			enlightenPanel.run = FindAndName(panelAsset.transform, "Run").GetComponent<Button>();
			enlightenPanel.gradient = FindAndName(panelAsset.transform, "Gradient", "Enable Gradient Mode").GetComponent<Button>();
			enlightenPanel.gradientPanel = panelAsset.transform.Find("GradientPanel").gameObject;
			var gradientPanel = enlightenPanel.gradientPanel.transform;
			enlightenPanel.gradientStart = FindAndName(gradientPanel, "Start", "Start of Selection").GetComponent<Button>();
			enlightenPanel.gradientEnd = FindAndName(gradientPanel, "End", "End of Selection").GetComponent<Button>();
			enlightenPanel.gradientEasing = gradientPanel.Find("Easings").GetComponent<Dropdown>();
			enlightenPanel.exitGradient = gradientPanel.Find("ExitGradient").GetComponent<Button>();
			var endButtons = gradientPanel.Find("EndButtons");
			enlightenPanel.gradientSwap = FindAndName(endButtons, "Swap", "Swap Gradient Ends").GetComponent<Button>();
			enlightenPanel.gradientParallel = FindAndName(endButtons, "Parallel", "Run Light Groups Individually").GetComponent<Button>();
			enlightenPanel.gradientClone = FindAndName(endButtons, "Clone", "Apply All To Other End").GetComponent<Button>();
			var buttonsObj = enlightenPanel.transform.Find("MainButtons");
			enlightenPanel.deleteAll = FindAndName(buttonsObj, "Delete", "Delete All").GetComponent<Button>();
			enlightenPanel.reloadAll = FindAndName(buttonsObj, "Reload", "Default All").GetComponent<Button>();

			enlightenPanel.gradientEasing.ClearOptions();

			foreach (var key in Easing.DisplayNameToInternalName.Keys)
			{
				enlightenPanel.gradientEasing.options.Add(new Dropdown.OptionData(key));
			}
		}

		public void OnLoad()
		{
			var mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			canvas = mapEditorUI.MainUIGroup[5].transform;

			panel = UnityEngine.Object.Instantiate(panelAsset, canvas).GetComponent<EnlightenPanel>();
			panel.Initialize();
			panel.run.onClick.AddListener(Run);
			panel.gameObject.SetActive(false);
		}

		private void OnPress()
		{
			panel.gameObject.SetActive(!panel.isActiveAndEnabled);
		}

		private static void Dialogue(string message)
		{
			PersistentUI.Instance.ShowDialogBox(message, null, PersistentUI.DialogBoxPresetType.Ok);
		}

		public static bool DataContainsOption(Dictionary<string, float> data, OptionName option)
		{
			var name = Enlighten.OptionToString(option);
			return data.Keys.Any(x => x.Contains(name));
		}

		private bool IsOptionOn(OptionName option, bool eitherStartOrEnd = true)
		{
			var onCurrently = panel.optionButtons[option].on;

			if (!eitherStartOrEnd) return onCurrently;

			if (panel.isGradient)
			{
				return panel.startEnabledOptions.Contains(option) || panel.endEnabledOptions.Contains(option);
			}

			return onCurrently;
		}

		private SliderParameter GetParameter(OptionName option, string parameter)
		{
			if (!panel.optionPanels[option].parameters.ContainsKey(parameter)) return null;

			return panel.optionPanels[option].parameters[parameter];
		}

		private float GetParameterValue(SliderParameter param, float t)
		{
			var value = param.value;

			if (panel.isGradient)
			{
				var key = param.GetValueName();
				var defaultValue = param.defaultValue;

				bool onInStart =
					panel.startEnabledOptions.Contains(param.optionPanel.optionName);

				bool onInEnd =
					panel.endEnabledOptions.Contains(param.optionPanel.optionName);

				var startAmount = onInStart ? panel.startOptionValues[key] : defaultValue;
				var endAmount = onInEnd ? panel.endOptionValues[key] : defaultValue;

				value = Mathf.Lerp(startAmount, endAmount, t);
			}

			return value;
		}

		private List<Func<Color, float, float, Color>> MakeColorProcess()
		{
			var colorProcess = new List<Func<Color, float, float, Color>>();

			if (IsOptionOn(OptionName.Brightness))
			{
				var param = GetParameter(OptionName.Brightness, "Multiplier");

				colorProcess.Add((Color color, float t, float tOriginal) =>
				{
					var value = GetParameterValue(param, t);

					color.r *= value;
					color.g *= value;
					color.b *= value;

					return color;
				});
			}

			if (IsOptionOn(OptionName.Alpha))
			{
				var param = GetParameter(OptionName.Alpha, "Multiplier");

				colorProcess.Add((Color color, float t, float tOriginal) =>
				{
					var value = GetParameterValue(param, t);

					color.a *= value;

					return color;
				});
			}

			if (IsOptionOn(OptionName.Hue))
			{
				var param = GetParameter(OptionName.Hue, "Offset");

				colorProcess.Add((Color color, float t, float tOriginal) =>
				{
					var value = GetParameterValue(param, t);

					Color.RGBToHSV(color, out float H, out float S, out float V);

					H = (H + value) % 1;

					if ((H < 0 && 1 > 0) || (H > 0 && 1 < 0))
					{
						H += 1;
					}

					var col = Color.HSVToRGB(H, S, V);
					col.a = color.a;

					return col;
				});
			}

			if (IsOptionOn(OptionName.Saturation))
			{
				var param = GetParameter(OptionName.Saturation, "Offset");

				colorProcess.Add((Color color, float t, float tOriginal) =>
				{
					var value = GetParameterValue(param, t);

					Color.RGBToHSV(color, out float H, out float S, out float V);

					S = Mathf.Clamp01(S + value);

					var col = Color.HSVToRGB(H, S, V);
					col.a = color.a;

					return col;
				});
			}

			if (IsOptionOn(OptionName.Pulse))
			{
				var intensityParam = GetParameter(OptionName.Pulse, "Intensity");
				var cyclesParam = GetParameter(OptionName.Pulse, "Cycles");

				var startValue = GetParameterValue(cyclesParam, 0);
				var endValue = GetParameterValue(cyclesParam, 1);

				colorProcess.Add((Color color, float t, float tOriginal) =>
				{
					var intensity = GetParameterValue(intensityParam, t);

					bool isReverse = panel.isGradient && startValue > endValue;
					var periodTime = isReverse ? 1 - tOriginal : tOriginal;
					var period = isReverse ?
						Mathf.Lerp(endValue, startValue, periodTime)
						: GetParameterValue(cyclesParam, periodTime);
					var x = period * (periodTime - 0.25f / period) * (float)Math.PI * 2;

					var val = (Mathf.Sin(x) * 0.5f + 0.5f) * intensity;

					color.r += val;
					color.g += val;
					color.b += val;

					return color;
				});
			}

			return colorProcess;
		}

		private Color ClampColor(Color color)
		{
			if (color.r < 0) color.r = 0;
			if (color.g < 0) color.g = 0;
			if (color.b < 0) color.b = 0;
			if (color.a < 0) color.a = 0;
			return color;
		}

		private void Run()
		{
			var allEvents = SelectionController.SelectedObjects.OfType<BaseEvent>();

			if (allEvents.Count() == 0)
			{
				Dialogue("There are no events selected!");
				return;
			}

			bool optionsEnabled =
				panel.isGradient ?
				panel.startEnabledOptions.Count > 0 || panel.endEnabledOptions.Count > 0
				: panel.enabledOptions.Count > 0;

			if (!optionsEnabled)
			{
				Dialogue("There are no options enabled!");
				return;
			}

			allEvents = allEvents.OrderBy(x => x.JsonTime);

			var actions = new List<BeatmapAction>();
			var totalConflicting = new List<BaseObject>();
			var totalAdded = new List<BaseObject>();

			var colorProcess = MakeColorProcess();
			bool flutterOn = IsOptionOn(OptionName.Flutter);
			var flutterMultiplier = GetParameter(OptionName.Flutter, "Multiplier");
			var flutterTurbulence = GetParameter(OptionName.Flutter, "Turbulence");
			BaseObject lastEvent = null;
			var easingName = panel.gradientEasing.options.ElementAt(panel.gradientEasing.value).text;
			var easing = Easing.ByName[Easing.DisplayNameToInternalName[easingName]];

			Action<IEnumerable<BaseEvent>, float, float> doProcess = (IEnumerable<BaseEvent> events, float minTime, float maxTime) =>
			{
				var dist = maxTime - minTime;

				foreach (var e in events)
				{
					if (!e.IsLightEvent()) continue;
					var original = (BaseObject)e.Clone();
					
					if (!(e.CustomColor is Color color))
					{
						if (e.IsBlue) e.CustomColor = plugin.PlatformDescriptor.Colors.BlueColor;
						else e.CustomColor = plugin.PlatformDescriptor.Colors.RedColor;
						color = (Color)e.CustomColor;
					}

					var t = (e.JsonTime - minTime) / dist;
					var tOriginal = t;

					if (panel.isGradient)
					{
						t = easing(t);
					}

					foreach (var process in colorProcess)
					{
						color = process.Invoke(color, t, tOriginal);
					}

					e.CustomColor = ClampColor(color);
					e.WriteCustom();
					var modifyAction = new BeatmapObjectModifiedAction(e, e, original, "Modified with Enlighten", true);
					actions.Add(modifyAction);

					if (flutterOn && original.JsonTime != minTime)
					{
						var time = (e.JsonTime + lastEvent.JsonTime) / 2;
						t = (time - minTime) / dist;

						if (panel.isGradient)
						{
							t = easing(t);
						}

						var multiplier = GetParameterValue(flutterMultiplier, t);
						var turbulence = GetParameterValue(flutterTurbulence, t);

						var value = multiplier + UnityEngine.Random.Range(-turbulence, turbulence);
						var eventCopy = (BaseObject)e.Clone();
						var colorCopy = (Color)eventCopy.CustomColor;

						colorCopy.r *= value;
						colorCopy.g *= value;
						colorCopy.b *= value;

						eventCopy.JsonTime = time;
						eventCopy.CustomColor = ClampColor(colorCopy);
						eventCopy.WriteCustom();

						totalAdded.Add(eventCopy);
						plugin.events.SpawnObject(eventCopy, out var conflicting, true, true, true);
						totalConflicting.AddRange(conflicting);
					}

					lastEvent = e;
				}
			};

			if (panel.parallel && panel.isGradient)
			{
				var groups = new Dictionary<int, List<BaseEvent>>();

				foreach (var eventObj in allEvents)
				{
					if (!groups.ContainsKey(eventObj.Type))
					{
						var group = new List<BaseEvent>();
						groups[eventObj.Type] = group;
					}

					groups[eventObj.Type].Add(eventObj);
				}

				foreach (var group in groups.Values)
				{
					var minTime = group.First().JsonTime;
					var maxTime = group.Last().JsonTime;
					doProcess(group, minTime, maxTime);
				}
			}
			else
			{
				var minTime = allEvents.First().JsonTime;
				var maxTime = allEvents.Last().JsonTime;
				doProcess(allEvents, minTime, maxTime);
			}

			if (flutterOn)
			{
				actions.Add(new BeatmapObjectPlacementAction(totalAdded, totalConflicting, "Added with Enlighten"));
				plugin.events.DoPostObjectsSpawnedWorkflow();
			}

			var allActions = new ActionCollectionAction(actions, false, false, "Enlighten");
			BeatmapActionContainer.AddAction(allActions);

			plugin.events.RefreshEventsAppearance(allEvents);
		}

		public static void AddTooltip(GameObject gameObject, string tooltip)
		{
			var component = gameObject.AddComponent<Tooltip>();
			component.TooltipOverride = tooltip;
		}

		public static Transform FindAndName(Transform baseObj, string name, string tooltip = null)
		{
			var obj = baseObj.Find(name);
			UI.AddTooltip(obj.gameObject, tooltip ?? name);
			return obj;
		}
	}
}
