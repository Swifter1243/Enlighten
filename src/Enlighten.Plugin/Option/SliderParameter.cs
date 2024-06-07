using Enlighten.src.Enlighten.Plugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin.Option
{
	public class SliderParameter : TypedOptionParameter<float>
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
			FindDefault();
			slider.minValue = min;
			slider.maxValue = max;
			inputField.onValueChanged.AddListener(ApplyInputFieldToProperty);
			onValueChanged.AddListener(ApplyPropertyToUI);
		}

		public void ApplySliders()
		{
			FindDefault();
			slider.minValue = min;
			slider.maxValue = max;
			ToDefault();
		}

		public void FindDefault()
		{
			float value = slider.value;
			value -= slider.minValue;
			value /= slider.maxValue - slider.minValue;
			defaultValue = value;
		}

		void ApplyPropertyToUI(float newValue)
		{
			ApplyPropertyToSlider(newValue);
			ApplyPropertyToInputField(newValue);
		}

		void ApplyPropertyToSlider(float newValue)
		{
			slider.SetValueWithoutNotify(newValue);
		}

		void ApplyPropertyToInputField(float newValue)
		{
			string value = newValue.ToString();
			inputField.SetTextWithoutNotify(value);
		}

		void ApplySliderToProperty(float value)
		{
			propertyValue = value;
		}

		void ApplyInputFieldToProperty(string text)
		{
			if (float.TryParse(text, out float value))
			{
				propertyValue = value;
			}
		}

		public override void ReadData(OptionParameterData data)
		{
			if (data is SliderParameterData sliderData)
			{
				propertyValue = sliderData.value;
			}
			else
			{
				throw new TypeAccessException("ReadData on SliderParameter can only read SliderParameterData");
			}
		}

		public override void WriteData(OptionParameterData data)
		{
			if (data is SliderParameterData sliderData)
			{
				sliderData.value = propertyValue;
			}
			else
			{
				throw new TypeAccessException("WriteData on SliderParameter can only write to SliderParameterData");
			}
		}
	}
}
