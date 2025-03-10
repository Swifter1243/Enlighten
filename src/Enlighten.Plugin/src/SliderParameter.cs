using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin
{
	public class SliderParameter : MonoBehaviour
	{
		public Slider slider;
		public InputField inputField;
		public OptionPanel optionPanel;

		public float min;
		public float max;
		public float defaultValue;
		public float value;
		public string property;
		public bool skipWrite;

		public float FromSliderValue() => FromSliderValue(slider.value);
		public float FromSliderValue(float val)
		{
			val -= slider.minValue;
			val /= slider.maxValue - slider.minValue;
			return Mathf.Lerp(min, max, val);
		}

		public void ToSliderValue(float val)
		{
			val -= min;
			val /= max - min;
			slider.SetValueWithoutNotify(Mathf.Lerp(slider.minValue, slider.maxValue, val));
			OnValueChange();
		}

		public void OnSliderChange(float val)
		{
			value = FromSliderValue(val);
			inputField.SetTextWithoutNotify(value.ToString());
			OnValueChange();
		}

		public void OnInputFieldChange(string val)
		{
			if (float.TryParse(val, out float f))
			{
				value = f;
				ToSliderValue(value);
			}
		}

		public void OnValueChange()
		{
			if (skipWrite)
			{
				skipWrite = false;
			}
			else
			{
				optionPanel.WriteToValues(optionPanel.enlightenPanel.optionValues);
			}

			optionPanel.CheckDefaultState();
			optionPanel.CheckReflect();
		}

		public void SetValue(float val)
		{
			value = val;
			ToSliderValue(value);
			inputField.SetTextWithoutNotify(value.ToString());
		}

		public bool IsDefault()
		{
			return Math.Abs(value - defaultValue) < 0.001;
		}

		public void ToDefault()
		{
			SetValue(defaultValue);
		}

		public string GetValueName() => Enlighten.OptionToString(optionPanel.optionName) + property;
	}

	public class SliderParameterInitializer
	{
		public float min;
		public float max;
		public string property;
		public string tooltip;

		public SliderParameterInitializer(string property, string tooltip, float min, float max)
		{
			this.property = property;
			this.tooltip = tooltip;
			this.min = min;
			this.max = max;
		}
	}
}
