using Enlighten.src.Enlighten.Plugin.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enlighten.src.Enlighten.Plugin.Data
{
	public class OptionDataHolder
	{
		public Dictionary<OptionName, Dictionary<string, OptionParameterData>> data =
			new Dictionary<OptionName, Dictionary<string, OptionParameterData>>()
		{
			{  OptionName.Brightness, new Dictionary <string, OptionParameterData>()
				{
					{ "Multiplier", new SliderParameterData(0) }
				}
			},
			{  OptionName.Brightness, new Dictionary <string, OptionParameterData>()
				{
					{ "Multiplier", new SliderParameterData(0) }
				}
			},
		};
	}

	public class SliderParameterData : OptionParameterData
	{
		public float value;
		public SliderParameterData(float value)
		{
			this.value = value;
		}
	}

	public abstract class OptionParameterData
	{
	}
}
