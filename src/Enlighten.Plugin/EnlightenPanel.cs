﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin
{
	public class EnlightenPanel : MonoBehaviour
	{
		public Button gradient;
		public GameObject gradientPanel;
		public Button exitGradient;
		public Button gradientStart;
		public Button gradientEnd;
		public Image gradientStartImage;
		public Image gradientEndImage;
		public Dropdown gradientEasing;
		public Button run;
		public Button gradientSwap;
		public Button gradientParallel;
		public Button gradientClone;
		public Button deleteAll;
		public Button reloadAll;

		public Dictionary<OptionName, OptionPanel> optionPanels = new Dictionary<OptionName, OptionPanel>();
		public Dictionary<OptionName, OptionButton> optionButtons = new Dictionary<OptionName, OptionButton>();

		public static IReadOnlyDictionary<OptionName, SliderParameterInitializer[]> parameterLookup =
			new Dictionary<OptionName, SliderParameterInitializer[]>()
		{
			{ OptionName.Brightness, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Multiplier", "Multiplies Event Brightness", 0, 2)
			}},
			{ OptionName.Alpha, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Multiplier", "Multiplies Event Alpha", 0, 2)
			}},
			{ OptionName.Hue, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Offset", "Adds to Event Hue", -1, 1)
			}},
			{ OptionName.Saturation, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Offset", "Adds to Event Saturation", -1, 1)
			}},
			{ OptionName.Flutter, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Multiplier", "Multiplies Flicker Brightness", 0, 2),
				new SliderParameterInitializer("Turbulence", "Randomly Varies Flicker Brightness", 0, 1)
			}},
			{ OptionName.Pulse, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Intensity", "Brightness of Pulse", -1, 1),
				new SliderParameterInitializer("Cycles", "Amount of Pulses", 1, 10)
			}},
		};

		public Dictionary<string, float> startOptionValues = new Dictionary<string, float>();
		public Dictionary<string, float> endOptionValues = new Dictionary<string, float>();
		public Dictionary<string, float> optionValues;

		public HashSet<OptionName> startEnabledOptions = new HashSet<OptionName>();
		public HashSet<OptionName> endEnabledOptions = new HashSet<OptionName>();
		public HashSet<OptionName> enabledOptions;

		public bool isGradient = false;
		public bool onStart = true;
		public bool parallel = false;

		public Dictionary<string, float> oppositeOptionValues
		{
			get => onStart ? endOptionValues : startOptionValues;
			set {
				if (onStart) endOptionValues = value;
				else startOptionValues = value;
			}
		}

		public HashSet<OptionName> oppositeEnabledOptions
		{
			get => onStart ? endEnabledOptions : startEnabledOptions;
			set {
				if (onStart) endEnabledOptions = value;
				else startEnabledOptions = value;
			}
		}

		public void WriteToValues(Dictionary<string, float> vals, bool onOnly = true)
		{
			vals.Clear();

			foreach (var button in optionButtons.Values)
			{
				if (button.on || !onOnly)
				{
					button.panel.WriteToValues(vals);
				}
			}
		}

		public void LoadValues(Dictionary<string, float> vals)
		{
			foreach (var button in optionButtons.Values)
			{
				button.panel.LoadValues(vals);
				button.LoadVisibility(enabledOptions);
			}
		}

		public void SwitchToValues(Dictionary<string, float> vals, HashSet<OptionName> enabled)
		{
			enabledOptions = enabled;
			optionValues = vals;
			LoadValues(optionValues);
		}

		public void Initialize()
		{
			// Initialize Buttons
			gradientStartImage = gradientStart.GetComponent<Image>();
			gradientEndImage = gradientEnd.GetComponent<Image>();
			UpdateGradientTab();

			gradient.onClick.AddListener(() =>
			{
				isGradient = true;
				gradientPanel.SetActive(true);
				gradient.gameObject.SetActive(false);
				SwitchToValues(optionValues, enabledOptions);
			});

			exitGradient.onClick.AddListener(() =>
			{
				isGradient = false;
				gradientPanel.SetActive(false);
				gradient.gameObject.SetActive(true);
				SwitchToValues(optionValues, enabledOptions);
			});

			gradientStart.onClick.AddListener(() =>
			{
				if (onStart) return;

				onStart = true;
				SwitchToValues(startOptionValues, startEnabledOptions);
				UpdateGradientTab();
			});

			gradientEnd.onClick.AddListener(() =>
			{
				if (!onStart) return;

				onStart = false;
				SwitchToValues(endOptionValues, endEnabledOptions);
				UpdateGradientTab();
			});

			gradientClone.onClick.AddListener(() =>
			{
				var oppositeEnabledOptions = this.oppositeEnabledOptions;
				var oppositeOptionValues = this.oppositeOptionValues;

				oppositeEnabledOptions.Clear();
				oppositeEnabledOptions.UnionWith(enabledOptions);
				oppositeOptionValues.Clear();

				foreach (var kvp in optionValues)
				{
					oppositeOptionValues.Add(kvp.Key, kvp.Value);
				}

				SwitchToValues(optionValues, enabledOptions);
			});

			gradientSwap.onClick.AddListener(() =>
			{
				var oppositeEnabledOptions = this.oppositeEnabledOptions;
				var oppositeOptionValues = this.oppositeOptionValues;

				// Swap values
				var tempDir = new Dictionary<string, float>();
				foreach (var kvp in optionValues)
				{
					tempDir.Add(kvp.Key, kvp.Value);
				}
				optionValues.Clear();
				foreach (var kvp in oppositeOptionValues)
				{
					optionValues.Add(kvp.Key, kvp.Value);
				}
				oppositeOptionValues.Clear();
				foreach (var kvp in tempDir)
				{
					oppositeOptionValues.Add(kvp.Key, kvp.Value);
				}

				// Swap enabled
				var tempSet = new HashSet<OptionName>();
				tempSet.UnionWith(enabledOptions);
				enabledOptions.Clear();
				enabledOptions.UnionWith(oppositeEnabledOptions);
				oppositeEnabledOptions.Clear();
				oppositeEnabledOptions.UnionWith(tempSet);

				SwitchToValues(optionValues, enabledOptions);
			});

			var parallelIcon = gradientParallel.GetComponentInChildren<RawImage>();

			gradientParallel.onClick.AddListener(() =>
			{
				parallel = !parallel;
				gradientParallel.image.color = parallel ? Color.gray : Color.white;
				parallelIcon.color = parallel ? Color.white : new Color(0, 0, 0, 0.8f);
			});

			reloadAll.onClick.AddListener(DefaultAll);
			deleteAll.onClick.AddListener(DeleteAll);

			optionValues = startOptionValues;
			enabledOptions = startEnabledOptions;

			// Hooking up UI
			var panelsObj = transform.Find("OptionPanels").Find("Viewport").Find("Content");
			var buttonsObj = transform.Find("OptionButtons");

			foreach (var name in Enum.GetNames(typeof(OptionName)))
			{
				var enumKey = Enlighten.StringToOption(name);

				// Option Panels
				var panelObj = panelsObj.Find(name);
				Debug.Log(panelObj);
				var panel = panelObj.gameObject.AddComponent<OptionPanel>();
				panel.enlightenPanel = this;
				panel.optionName = enumKey;
				var panelButtons = panel.transform.Find("Buttons");
				panel.reload = UI.FindAndName(panelButtons, "Reload", "Return to Defaults").GetComponent<Button>();
				panel.delete = UI.FindAndName(panelButtons, "Delete").GetComponent<Button>();
				panel.hide = UI.FindAndName(panelButtons, "Hide", "Disable").GetComponent<Button>();
				panel.reflect = UI.FindAndName(panelButtons, "Reflect", "Apply to Other End").GetComponent<Button>();
				panel.reload.onClick.AddListener(panel.Reload);
				panel.reflect.onClick.AddListener(panel.Reflect);
				panel.InitializeParameters(parameterLookup[enumKey]);
				optionPanels.Add(enumKey, panel);

				// Option Buttons
				var buttonObj = UI.FindAndName(buttonsObj, name);
				var button = buttonObj.gameObject.AddComponent<OptionButton>();
				button.enlightenPanel = this;
				button.optionName = enumKey;
				button.panel = panel;
				button.image = button.GetComponentInChildren<RawImage>();
				button.button = button.GetComponent<Button>();
				button.buttonImage = button.GetComponent<Image>();
				button.button.onClick.AddListener(button.ClickToggle);
				panel.delete.onClick.AddListener(button.ClickDelete);
				panel.hide.onClick.AddListener(button.ClickHide);
				optionButtons.Add(enumKey, button);
			}

			LoadValues(optionValues);
			WriteToValues(startOptionValues, false);
			WriteToValues(endOptionValues, false);
		}

		public void UpdateGradientTab()
		{
			gradientStartImage.color = onStart ? Color.gray : Color.white;
			gradientEndImage.color = !onStart ? Color.gray : Color.white;
		}

		public void CheckEndSimilarity()
		{
			var notSimilar = optionPanels.Values.Any(x => x.CanReflect());
			gradientClone.gameObject.SetActive(notSimilar);
			gradientSwap.gameObject.SetActive(notSimilar);
		}

		public void CheckDefaultAll()
		{
			var canDefault = optionPanels.Values.Any(x => !x.IsDefault());
			reloadAll.gameObject.SetActive(canDefault);
		}

		public void DefaultAll()
		{
			foreach (var panel in optionPanels.Values)
			{
				panel.ToDefault();
			}

			reloadAll.gameObject.SetActive(false);
		}

		public void CheckDeleteAll()
		{
			var canDelete = enabledOptions.Count > 0;
			deleteAll.gameObject.SetActive(canDelete);
		}

		public void DeleteAll()
		{
			foreach (var button in optionButtons.Values)
			{
				button.Clear();
			}

			deleteAll.gameObject.SetActive(false);
		}
	}
}
