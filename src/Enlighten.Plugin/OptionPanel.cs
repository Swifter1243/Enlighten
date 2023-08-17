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

				slider.option = this;
				slider.slider = slider.GetComponentInChildren<Slider>();
				slider.inputfield = slider.GetComponentInChildren<InputField>();
				slider.defaultValue = slider.FromSliderValue();
				slider.ToDefault();

				slider.inputfield.gameObject.AddComponent<DisableActionsField>();
				slider.slider.onValueChanged.AddListener(slider.OnSliderChange);
				slider.inputfield.onValueChanged.AddListener(slider.OnInputFieldChange);

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

		public void ToDefault()
		{
			foreach (var val in parameters.Values)
			{
				val.ToDefault();
			}
		}

		public void CheckDefaultState()
		{
			bool changed = parameters.Values.Any(x => !x.IsDefault());
			reload.gameObject.SetActive(changed);
		}
	}
}
