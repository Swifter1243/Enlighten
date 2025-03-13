using System.Collections.Generic;
using Beatmap.Base;
using UnityEngine;
namespace Enlighten.Core
{
	public class AlphaEffect : SimpleColorEffect
	{
		private readonly FloatParameter m_multiplier = new FloatParameter(1, "Multiplier", "The amount to multiply event's alpha channel in their color by.");

		public AlphaEffect(string name, string description) : base(name, description)
		{ }

		protected override IEnumerable<BaseParameter> GetParameters()
		{
			yield return m_multiplier;
		}

		protected override Color GetColor(float normalTime, Color color)
		{
			float multiplier = m_multiplier.Interpolate(normalTime);
			color.a *= multiplier;
			return color;
		}
	}
}
