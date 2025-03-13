﻿using System.Collections.Generic;
using UnityEngine;
namespace Enlighten.Core
{
	public class BrightnessEffect : SimpleColorEffect
	{
		private readonly FloatParameter m_multiplier = new FloatParameter(1, "Multiplier", "The amount to multiply events' brightness by.");

		public BrightnessEffect(string name, string description) : base(name, description)
		{ }

		protected override IEnumerable<BaseParameter> GetParameters()
		{
			yield return m_multiplier;
		}

		protected override Color GetColor(float normalTime, Color color)
		{
			float multiplier = m_multiplier.Interpolate(normalTime);
			color.r *= multiplier;
			color.g *= multiplier;
			color.b *= multiplier;
			return color;
		}
	}
}
