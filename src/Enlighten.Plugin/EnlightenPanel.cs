using System;
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
		public Button run;

		public Dictionary<OptionName, OptionPanel> optionPanels = new Dictionary<OptionName, OptionPanel>();
		public Dictionary<OptionName, OptionButton> optionButtons = new Dictionary<OptionName, OptionButton>();

		public static IReadOnlyDictionary<OptionName, SliderParameterInitializer[]> parameterLookup =
			new Dictionary<OptionName, SliderParameterInitializer[]>()
		{
			{ OptionName.Brightness, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Amount", 0, 2)
			}},
			{ OptionName.Alpha, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Amount", 0, 2)
			}},
			{ OptionName.Hue, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Offset", -1, 1)
			}},
			{ OptionName.Saturation, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Offset", -1, 1)
			}},
			{ OptionName.Flutter, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Intensity", 0, 2),
				new SliderParameterInitializer("Turbulence", -1, 1)
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

		public void WriteToValues(Dictionary<string, float> vals)
		{
			vals.Clear();

			foreach (var button in optionButtons.Values)
			{
				if (button.on)
				{
					button.panel.WriteToValues(vals);
				}
			}
		}

		public void LoadValues(Dictionary<string, float> vals)
		{
			foreach (var button in optionButtons.Values)
			{
				button.LoadVisibility(enabledOptions);
				button.panel.LoadValues(vals);
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

				if (!onStart)
				{
					SwitchToValues(endOptionValues, endEnabledOptions);
				}
			});

			exitGradient.onClick.AddListener(() =>
			{
				isGradient = false;
				gradientPanel.SetActive(false);
				gradient.gameObject.SetActive(true);
				SwitchToValues(startOptionValues, startEnabledOptions);
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

			optionValues = startOptionValues;
			enabledOptions = startEnabledOptions;

			// Hooking up UI
			var panelsObj = transform.Find("OptionPanels");
			var buttonsObj = transform.Find("OptionButtons");

			foreach (var name in Enum.GetNames(typeof(OptionName)))
			{
				var enumKey = Enlighten.StringToOption(name);

				// Option Panels
				var panelObj = panelsObj.Find(name);
				var panel = panelObj.gameObject.AddComponent<OptionPanel>();
				panel.enlightenPanel = this;
				panel.optionName = enumKey;
				panel.reload = panel.transform.Find("Reload").GetComponent<Button>();
				panel.delete = panel.transform.Find("Delete").GetComponent<Button>();
				panel.reload.onClick.AddListener(panel.ToDefault);
				panel.InitializeParameters(parameterLookup[enumKey]);
				optionPanels.Add(enumKey, panel);

				// Option Buttons
				var buttonObj = buttonsObj.Find(name);
				var button = buttonObj.gameObject.AddComponent<OptionButton>();
				button.enlightenPanel = this;
				button.optionName = enumKey;
				button.panel = panel;
				button.image = button.GetComponentInChildren<RawImage>();
				button.button = button.GetComponent<Button>();
				button.buttonImage = button.GetComponent<Image>();
				button.button.onClick.AddListener(button.Toggle);
				panel.delete.onClick.AddListener(button.Clear);
				optionButtons.Add(enumKey, button);
			}

			LoadValues(optionValues);
		}

		public void UpdateGradientTab()
		{
			gradientStartImage.color = onStart ? Color.gray : Color.white;
			gradientEndImage.color = !onStart ? Color.gray : Color.white;
		}
	}
}
