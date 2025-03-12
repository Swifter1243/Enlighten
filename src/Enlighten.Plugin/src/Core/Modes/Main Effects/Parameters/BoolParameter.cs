﻿namespace Enlighten.Core
{
	public class BoolParameter : TypedParameter<bool>
	{
		public BoolParameter(bool defaultValue) : base(defaultValue)
		{ }

		protected override bool InterpolatePoints(int left, int right, float normalTime)
		{
			return m_keyframes[left].m_value;
		}
	}
}
