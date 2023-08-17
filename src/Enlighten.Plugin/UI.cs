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
			return panel.optionPanels[option].parameters[parameter];
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

			foreach (var e in events)
			{
				if (!(e.CustomColor is Color color)) continue;

				var t = (e.JsonTime - minTime) / dist;
				var original = (BaseObject)e.Clone();

				if (IsOptionOn(OptionName.Brightness))
				{
					var param = GetParameter(OptionName.Brightness, "Amount");
					var amount = param.value;

					if (panel.isGradient)
					{
						var key = param.GetValueName();
						var defaultValue = param.defaultValue;

						bool onInStart =
							panel.startEnabledOptions.Contains(OptionName.Brightness);

						bool onInEnd =
							panel.endEnabledOptions.Contains(OptionName.Brightness);

						var startAmount = onInStart ? panel.startOptionValues[key] : defaultValue;
						var endAmount = onInEnd ? panel.endOptionValues[key] : defaultValue;

						amount = Mathf.Lerp(startAmount, endAmount, t);
					}

					color.r *= amount;
					color.g *= amount;
					color.b *= amount;
				}

				e.CustomColor = color;
				e.WriteCustom();

				var action = new BeatmapObjectModifiedAction(e, e, original, "Modified with Enlighten.", true);
				actions.Add(action);
			}

			var allActions = new ActionCollectionAction(actions, false, false, "Modified with Enlighten.");
			BeatmapActionContainer.AddAction(allActions);

			plugin.events.RefreshEventsAppearance(events);
		}

		private void TestProcess()
		{
			var actions = new List<BeatmapAction>();
			var events = plugin.events.LoadedObjects.Cast<BaseEvent>();

			foreach (var light in events)
			{
				if (light.CustomColor != null)
				{
					var original = (BaseObject)light.Clone();
					light.CustomColor *= 3;
					light.WriteCustom();

					var action = new BeatmapObjectModifiedAction(light, light, original, "Modified with Enlighten.", true);
					actions.Add(action);
				}
			}

			var allActions = new ActionCollectionAction(actions, false, false, "Modified with Enlighten.");
			BeatmapActionContainer.AddAction(allActions);

			plugin.events.RefreshEventsAppearance(events);
		}
	}
}
