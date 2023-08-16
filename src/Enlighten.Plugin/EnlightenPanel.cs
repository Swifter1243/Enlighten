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
		public Button run;
		public Dictionary<OptionNames, EnlightenOption> optionPanels = new Dictionary<OptionNames, EnlightenOption>();

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

		public Dictionary<string, float> startVals = new Dictionary<string, float>();
		public Dictionary<string, float> endVals = new Dictionary<string, float>();
		public Dictionary<string, float> currVals;

		public void WriteToValues(Dictionary<string, float> vals)
		{
			foreach (var param in optionPanels.Values)
			{
				param.WriteToValues(vals);
			}
		}

		public void LoadValues(Dictionary<string, float> vals)
		{
			foreach (var param in optionPanels.Values)
			{
				param.LoadValues(vals);
			}
		}

		public void Initialize()
		{
			currVals = startVals;
			var panelsObj = transform.Find("Option Panels");

			foreach (var name in Enum.GetNames(typeof(OptionNames)))
			{
				var enumKey = (OptionNames)Enum.Parse(typeof(OptionNames), name);
				var optionObj = panelsObj.Find(name + "Panel");
				var component = optionObj.gameObject.AddComponent<EnlightenOption>();
				component.optionName = name;
				component.reload = component.GetComponentInChildren<Button>();
				component.InitializeParameters(parameterLookup[enumKey]);
				component.reload.onClick.AddListener(component.ToDefault);
				optionPanels.Add(enumKey, component);
			}
		}
	}
}
