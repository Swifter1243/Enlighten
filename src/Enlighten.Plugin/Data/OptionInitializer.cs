using Enlighten.src.Enlighten.Plugin.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin.Data
{
	public class SliderInitializer : OptionInitializer
	{
		public float min;
		public float max;

		public SliderInitializer(string tooltip, float min, float max)
		{
			this.tooltip = tooltip;
			this.min = min;
			this.max = max;
		}

		public void Initialize(SliderParameter slider)
		{
			slider.min = min;
			slider.max = max;
			UI.AddTooltip(slider.gameObject, tooltip);
		}
	}


	public abstract class OptionInitializer
	{
		public string tooltip;
	}
}
