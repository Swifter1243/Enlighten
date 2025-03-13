using UnityEngine;
namespace Enlighten.Core
{
	public class RangeParameter : FloatParameter
	{
		public readonly float m_minValue;
		public readonly float m_maxValue;

		public RangeParameter(float minValue, float maxValue, float defaultValue, string name, string description) : base(defaultValue, name, description)
		{
			m_minValue = minValue;
			m_maxValue = maxValue;
		}

		protected override float InterpolatePoints(int left, int right, float normalTime)
		{
			float value = base.InterpolatePoints(left, right, normalTime);
			return Mathf.Clamp(value, m_minValue, m_maxValue);
		}
	}
}
