using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin.Option
{
	public class SliderParameter : OptionParameter<float>
	{
		public Slider slider;
		public InputField inputField;
		public float min;
		public float max;

		override public bool IsDefault()
		{
			return Math.Abs(propertyValue - defaultValue) < 0.001f;
		}

		void Awake()
		{
			slider = GetComponentInChildren<Slider>();
			inputField = GetComponentInChildren<InputField>();
			slider.onValueChanged.AddListener(ApplySliderToProperty);
			inputField.onValueChanged.AddListener(ApplyInputFieldToProperty);
			onValueChanged.AddListener(ApplyPropertyToUI);
		}

		void ApplyPropertyToUI(float newValue)
		{
			ApplyPropertyToSlider(newValue);
			ApplyPropertyToInputField(newValue);
		}

		void ApplyPropertyToSlider(float newValue)
		{
			float value = PropertyToSliderValue(newValue);
			slider.SetValueWithoutNotify(value);
		}

		void ApplyPropertyToInputField(float newValue)
		{
			string value = newValue.ToString();
			inputField.SetTextWithoutNotify(value);
		}

		float SliderToPropertyValue(float value)
		{
			value -= slider.minValue;
			value /= slider.maxValue - slider.minValue;
			return Mathf.Lerp(min, max, value);
		}

		float PropertyToSliderValue(float value)
		{
			value -= min;
			value /= max - min;
			return Mathf.Lerp(slider.minValue, slider.maxValue, value);
		}

		void ApplySliderToProperty(float value)
		{
			propertyValue = SliderToPropertyValue(value);
		}

		void ApplyInputFieldToProperty(string text)
		{
			if (float.TryParse(text, out float value))
			{
				propertyValue = value;
			}
		}
	}
}
