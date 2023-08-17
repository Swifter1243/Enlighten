using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using UnityEngine;
using Beatmap.Base;
using System.IO;
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
			ExtensionButtons.AddButton(button);

			// UI Setup
			panelAsset = plugin.bundle.LoadAsset<GameObject>("Assets/EnlightenPanel.prefab");
			var enlightenPanel = panelAsset.AddComponent<EnlightenPanel>();

			enlightenPanel.run = panelAsset.transform.Find("Run").GetComponent<Button>();
			enlightenPanel.gradient = panelAsset.transform.Find("Gradient").GetComponent<Button>();
			enlightenPanel.gradientPanel = panelAsset.transform.Find("GradientPanel").gameObject;
			enlightenPanel.gradientStart = enlightenPanel.gradientPanel.transform.Find("Start").GetComponent<Button>();
			enlightenPanel.gradientEnd = enlightenPanel.gradientPanel.transform.Find("End").GetComponent<Button>();
			enlightenPanel.exitGradient = enlightenPanel.gradientPanel.transform.Find("ExitGradient").GetComponent<Button>();
		}

		public void OnLoad()
		{
			var mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			canvas = mapEditorUI.MainUIGroup[5].transform;

			panel = UnityEngine.Object.Instantiate(panelAsset, canvas).GetComponent<EnlightenPanel>();
			panel.transform.localScale = new Vector3(1, 1, 1);
			panel.Initialize();
			panel.run.onClick.AddListener(Run);
			panel.gameObject.SetActive(false);

			//var rect = ((RectTransform)panel.transform);
			//rect.offsetMax = new Vector2(-780, rect.offsetMax.y);
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
					panel.startEnabledOptions.Contains(param.option.optionName);

				bool onInEnd =
					panel.endEnabledOptions.Contains(param.option.optionName);

				var startAmount = onInStart ? panel.startOptionValues[key] : defaultValue;
				var endAmount = onInEnd ? panel.endOptionValues[key] : defaultValue;

				value = Mathf.Lerp(startAmount, endAmount, t);
			}

			return value;
		}

		private List<Func<Color, float, Color>> MakeColorProcess()
		{
			var colorProcess = new List<Func<Color, float, Color>>();

			if (IsOptionOn(OptionName.Brightness))
			{
				colorProcess.Add((Color color, float t) =>
				{
					var param = GetParameter(OptionName.Brightness, "Amount");
					var value = GetParameterValue(param, t);

					color.r *= value;
					color.g *= value;
					color.b *= value;

					return color;
				});
			}

			if (IsOptionOn(OptionName.Alpha))
			{
				colorProcess.Add((Color color, float t) =>
				{
					var param = GetParameter(OptionName.Alpha, "Amount");
					var value = GetParameterValue(param, t);

					color.a *= value;

					return color;
				});
			}

			if (IsOptionOn(OptionName.Hue))
			{
				colorProcess.Add((Color color, float t) =>
				{
					var param = GetParameter(OptionName.Hue, "Offset");
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
				colorProcess.Add((Color color, float t) =>
				{
					var param = GetParameter(OptionName.Saturation, "Offset");
					var value = GetParameterValue(param, t);

					Color.RGBToHSV(color, out float H, out float S, out float V);

					S = Mathf.Clamp01(S + value);

					var col = Color.HSVToRGB(H, S, V);
					col.a = color.a;

					return col;
				});
			}

			return colorProcess;
		}

		private void Run()
		{
			var events = SelectionController.SelectedObjects.OfType<BaseEvent>();

			if (events.Count() == 0)
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

			events = events.OrderBy(x => x.JsonTime);

			var minTime = events.First().JsonTime;
			var maxTime = events.Last().JsonTime;
			var dist = maxTime - minTime;

			var actions = new List<BeatmapAction>();
			var totalConflicting = new List<BaseObject>();
			var totalAdded = new List<BaseObject>();

			var colorProcess = MakeColorProcess();
			bool flutterOn = IsOptionOn(OptionName.Flutter);
			var flutterIntensity = GetParameter(OptionName.Flutter, "Intensity");
			var flutterTurbulence = GetParameter(OptionName.Flutter, "Turbulence");
			BaseObject lastEvent = null;

			foreach (var e in events)
			{
				if (!(e.CustomColor is Color color)) continue;

				var t = (e.JsonTime - minTime) / dist;
				var original = (BaseObject)e.Clone();

				foreach (var process in colorProcess)
				{
					color = process.Invoke(color, t);
				}

				e.CustomColor = color;
				e.WriteCustom();
				var modifyAction = new BeatmapObjectModifiedAction(e, e, original, "Modified with Enlighten", true);
				actions.Add(modifyAction);

				if (flutterOn && original.JsonTime != minTime)
				{
					var time = (e.JsonTime + lastEvent.JsonTime) / 2;
					t = (time - minTime) / dist;

					var intensity = GetParameterValue(flutterIntensity, t);
					var turbulence = GetParameterValue(flutterTurbulence, t);

					var value = intensity + UnityEngine.Random.Range(-turbulence, turbulence);

					var eventCopy = (BaseObject)e.Clone();
					var colorCopy = (Color)eventCopy.CustomColor;

					colorCopy.r *= value;
					colorCopy.g *= value;
					colorCopy.b *= value;

					eventCopy.JsonTime = time;
					eventCopy.CustomColor = colorCopy;

					totalAdded.Add(eventCopy);
					plugin.events.SpawnObject(eventCopy, out var conflicting, true, true, true);
					totalConflicting.AddRange(conflicting);
				}

				lastEvent = e;
			}

			if (flutterOn)
			{
				actions.Add(new BeatmapObjectPlacementAction(totalAdded, totalConflicting, "Added with Enlighten"));
				plugin.events.DoPostObjectsSpawnedWorkflow();
			}

			var allActions = new ActionCollectionAction(actions, false, false, "Enlighten");
			BeatmapActionContainer.AddAction(allActions);

			plugin.events.RefreshEventsAppearance(events);
		}
	}
}
