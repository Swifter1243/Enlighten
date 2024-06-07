using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine.UI;
using UnityEngine;
using Enlighten.src.Enlighten.Plugin.Data;
using Enlighten.src.Enlighten.Plugin.Option;

namespace Enlighten.src.Enlighten.Plugin
{
	public class OptionPanel : MonoBehaviour
	{
		public Button reload;
		public Button delete;
		public Button hide;
		public Button reflect;
		public OptionName optionName;
		public Dictionary<string, OptionParameter> parameters = new Dictionary<string, OptionParameter>();

		void Awake()
		{
			var panelButtons = transform.Find("Buttons");
			reload = UI.FindAndName(panelButtons, "Reload", "Return to Defaults").GetComponent<Button>();
			delete = UI.FindAndName(panelButtons, "Delete").GetComponent<Button>();
			hide = UI.FindAndName(panelButtons, "Hide", "Disable").GetComponent<Button>();
			reflect = UI.FindAndName(panelButtons, "Reflect", "Apply to Other End").GetComponent<Button>();
		}

		public void InitializeParameters(
			Dictionary<string, OptionInitializer> initializers, 
			Dictionary<string, OptionParameterData> returnData
		)
		{
			foreach (var kvp in initializers)
			{
				var property = kvp.Key;
				var initializer = kvp.Value;
				var data = returnData[property];
				
				if (initializer is SliderInitializer sliderInitializer)
				{
					var slider = new SliderParameter();
					sliderInitializer.Initialize(slider);
					slider.ApplySliders();
					slider.WriteData(data);
					parameters[property] = slider;
				}
			}
		}

		public bool IsDefault() => !parameters.Values.Any(x => !x.IsDefault());
	}
}
