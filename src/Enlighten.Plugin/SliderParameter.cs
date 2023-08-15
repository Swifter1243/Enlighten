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
		public InputField inputfield;

		public float min;
		public float max;
		public float defaultVal;
		public string property;

		public float FromSliderValue()
		{
			float val = slider.value;
			val -= slider.minValue;
			val /= slider.maxValue - slider.minValue;
			return Mathf.Lerp(min, max, val);
		}

		public void ToSliderValue(float val)
		{
			val -= min;
			val /= max - min;
			slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, val);
		}
	}

	public class SliderParameterInitializer
	{
		public float min;
		public float max;
		public string property;

		public SliderParameterInitializer(string property, float min, float max)
		{
			this.property = property;
			this.min = min;
			this.max = max;
		}
	}
}
