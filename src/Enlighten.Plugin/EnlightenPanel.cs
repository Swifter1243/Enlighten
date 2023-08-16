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
		public Button run;

		public Dictionary<OptionNames, OptionPanel> optionPanels = new Dictionary<OptionNames, OptionPanel>();
		public Dictionary<OptionNames, OptionButton> optionButtons = new Dictionary<OptionNames, OptionButton>();

		public static IReadOnlyDictionary<OptionNames, SliderParameterInitializer[]> parameterLookup =
			new Dictionary<OptionNames, SliderParameterInitializer[]>()
		{
			{ OptionNames.Brightness, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Amount", 0, 2)
			}},
			{ OptionNames.Alpha, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Amount", 0, 2)
			}},
			{ OptionNames.Hue, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Offset", -1, 1)
			}},
			{ OptionNames.Saturation, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Offset", -1, 1)
			}},
			{ OptionNames.Flutter, new SliderParameterInitializer[]{
				new SliderParameterInitializer("Intensity", 0, 2),
				new SliderParameterInitializer("Turbulence", -1, 1)
			}},
		};

		public Dictionary<string, float> startOptions = new Dictionary<string, float>();
		public Dictionary<string, float> endOptions = new Dictionary<string, float>();
		public Dictionary<string, float> currentOptions;
		public bool isGradient = false;

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
				button.LoadValues(vals);
				button.panel.LoadValues(vals);
			}
		}

		public void Initialize()
		{
			gradient.onClick.AddListener(() =>
			{
				isGradient = true;
				gradientPanel.SetActive(true);
				gradient.gameObject.SetActive(false);
			});

			exitGradient.onClick.AddListener(() =>
			{
				isGradient = false;
				gradientPanel.SetActive(false);
				gradient.gameObject.SetActive(true);
				WriteToValues(currentOptions);
				currentOptions = startOptions;
				LoadValues(currentOptions);
			});

			currentOptions = startOptions;

			// Hooking up UI
			var panelsObj = transform.Find("OptionPanels");
			var buttonsObj = transform.Find("OptionButtons");

			foreach (var name in Enum.GetNames(typeof(OptionNames)))
			{
				var enumKey = (OptionNames)Enum.Parse(typeof(OptionNames), name);

				// Option Panels
				var panelObj = panelsObj.Find(name);
				var panel = panelObj.gameObject.AddComponent<OptionPanel>();
				panel.optionName = name;
				panel.reload = panel.transform.Find("Reload").GetComponent<Button>();
				panel.delete = panel.transform.Find("Delete").GetComponent<Button>();
				panel.reload.onClick.AddListener(panel.ToDefault);
				panel.InitializeParameters(parameterLookup[enumKey]);
				optionPanels.Add(enumKey, panel);

				// Option Buttons
				var buttonObj = buttonsObj.Find(name);
				var button = buttonObj.gameObject.AddComponent<OptionButton>();
				button.optionName = name;
				button.panel = panel;
				button.image = button.GetComponentInChildren<RawImage>();
				button.button = button.GetComponent<Button>();
				button.buttonImage = button.GetComponent<Image>();
				button.button.onClick.AddListener(button.Toggle);
				panel.delete.onClick.AddListener(button.Toggle);
				button.SetVisibility(false);
				optionButtons.Add(enumKey, button);
			}
		}
	}
}
