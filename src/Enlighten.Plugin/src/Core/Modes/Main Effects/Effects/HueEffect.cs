using System.Collections.Generic;
using UnityEngine;
namespace Enlighten.Core
{
	public class HueEffect : SimpleColorEffect
	{
		private readonly FloatParameter m_offset = new FloatParameter(0, "Offset", "How much to offset the of events' color.");

		public HueEffect(string name, string description) : base(name, description)
		{ }

		protected override IEnumerable<BaseParameter> GetParameters()
		{
			yield return m_offset;
		}

		private static float PositiveMod(float a, float b)
		{
			float c = a % b;
			return c < 0 ? c + b : c;
		}

		protected override Color GetColor(float normalTime, Color color)
		{
			float offset = m_offset.Interpolate(normalTime);

			Color.RGBToHSV(color, out float h, out float s, out float v);
			h = PositiveMod(h + offset, 1);
			return Color.HSVToRGB(h, s, v);
		}
	}
}
