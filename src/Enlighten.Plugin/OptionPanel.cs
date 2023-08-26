using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.UI;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin
{
	public class OptionPanel : MonoBehaviour
	{
		public Button reload;
		public Button delete;
		public Button hide;
		public Button reflect;
		public OptionName optionName;
		public Dictionary<string, SliderParameter> parameters = new Dictionary<string, SliderParameter>();
		public EnlightenPanel enlightenPanel;

		public void InitializeParameters(SliderParameterInitializer[] parameters)
		{
			foreach (var param in parameters)
			{
				var obj = transform.Find(param.property);

				var slider = obj.gameObject.AddComponent<SliderParameter>();
				slider.min = param.min;
				slider.max = param.max;
				slider.property = param.property;

				slider.optionPanel = this;
				slider.slider = slider.GetComponentInChildren<Slider>();
				slider.inputField = slider.GetComponentInChildren<InputField>();
				slider.defaultValue = slider.FromSliderValue();
				slider.ToDefault();

				slider.inputField.gameObject.AddComponent<DisableActionsField>();
				slider.slider.onValueChanged.AddListener(slider.OnSliderChange);
				slider.inputField.onValueChanged.AddListener(slider.OnInputFieldChange);

				this.parameters.Add(param.property, slider);
			}
		}

		public void WriteToValues(Dictionary<string, float> vals)
		{
			foreach (var param in parameters.Values)
			{
				vals[param.GetValueName()] = param.value;
			}
		}

		public void LoadValues(Dictionary<string, float> vals)
		{
			foreach (var param in parameters.Values)
			{
				param.skipWrite = true;

				if (vals.TryGetValue(param.GetValueName(), out float value))
				{
					param.SetValue(value);
				}
				else
				{
					param.SetValue(param.defaultValue);
				}
			}
		}

		public void Reload()
		{
			ToDefault(false);
		}

		public void ToDefault(bool both = false)
		{
			foreach (var val in parameters.Values)
			{
				val.ToDefault();

				if (both)
				{
					var key = val.GetValueName();
					enlightenPanel.oppositeOptionValues[key] = enlightenPanel.optionValues[key];
				}
			}
		}

		public bool IsDefault() => !parameters.Values.Any(x => !x.IsDefault());

		public void CheckDefaultState()
		{
			reload.gameObject.SetActive(!IsDefault());
			enlightenPanel.CheckDefaultAll();
		}

		public bool CanReflect()
		{
			bool canReflect = enlightenPanel.isGradient;

			if (canReflect)
			{
				canReflect = parameters.Values.Any(x =>
				{
					var key = x.GetValueName();
					var diff = enlightenPanel.startOptionValues[key] - enlightenPanel.endOptionValues[key];
					return Math.Abs(diff) > 0.001;
				});

				if (!canReflect)
				{
					canReflect =
						enlightenPanel.startEnabledOptions.Contains(optionName) ==
						!enlightenPanel.endEnabledOptions.Contains(optionName);
				}
			}

			return canReflect;
		}

		public void CheckReflect()
		{
			reflect.gameObject.SetActive(CanReflect());
			enlightenPanel.CheckEndSimilarity();
		}

		public void Reflect()
		{
			var opposite = enlightenPanel.oppositeOptionValues;

			foreach (var param in parameters.Values)
			{
				var key = param.GetValueName();
				opposite[key] = enlightenPanel.optionValues[key];
			}

			enlightenPanel.startEnabledOptions.Add(optionName);
			enlightenPanel.endEnabledOptions.Add(optionName);

			reflect.gameObject.SetActive(false);
			enlightenPanel.CheckEndSimilarity();
		}
	}
}
