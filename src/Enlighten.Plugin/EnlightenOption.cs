using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.UI;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin
{
	public class EnlightenOption : MonoBehaviour
	{
		public Button reload;
		public Dictionary<string, SliderParameter> parameters = new Dictionary<string, SliderParameter>();

		public void InitializeParameters(SliderParameterInitializer[] parameters)
		{
			foreach (var param in parameters)
			{
				var obj = transform.Find(param.property);

				var sliderComponent = obj.gameObject.AddComponent<SliderParameter>();
				sliderComponent.min = param.min;
				sliderComponent.max = param.max;

				sliderComponent.slider = sliderComponent.GetComponentInChildren<Slider>();
				sliderComponent.inputfield = sliderComponent.GetComponentInChildren<InputField>();
				sliderComponent.defaultVal = sliderComponent.FromSliderValue();

				this.parameters.Add(param.property, sliderComponent);
			}
		}
	}
}
