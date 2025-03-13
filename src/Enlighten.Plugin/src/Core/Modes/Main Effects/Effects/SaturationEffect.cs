using System.Collections.Generic;
using UnityEngine;
namespace Enlighten.Core
{
	public class SaturationEffect : SimpleColorEffect
	{
		private readonly FloatParameter m_offset = new FloatParameter(0, "Offset", "How much to offset events' color saturation by.");

		public SaturationEffect(string name, string description) : base(name, description)
		{ }

		protected override IEnumerable<BaseParameter> GetParameters()
		{
			yield return m_offset;
		}

		protected override Color GetColor(float normalTime, Color color)
		{
			float offset = m_offset.Interpolate(normalTime);

			Color.RGBToHSV(color, out float h, out float s, out float v);
			s = Mathf.Clamp01(s + offset);
			return Color.HSVToRGB(h, s, v);
		}
	}
}
