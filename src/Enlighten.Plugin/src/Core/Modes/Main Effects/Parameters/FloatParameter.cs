using UnityEngine;
namespace Enlighten.Core
{
	public class FloatParameter : GenericParameter<float>
	{
		public FloatParameter(float defaultValue, string name, string description) : base(defaultValue, name, description)
		{ }

		private float SplineInterpolation(int a, int b, float time)
		{
			float p1 = SortedKeyframes[a].m_value;
			float p2 = SortedKeyframes[b].m_value;
			float p0 = a - 1 < 0 ? p1 : SortedKeyframes[a - 1].m_value;
			float p3 = b + 1 > SortedKeyframes.Length - 1 ? p2 : SortedKeyframes[b + 1].m_value;

			float tt = time * time;
			float ttt = tt * time;

			float q0 = -ttt + (2.0f * tt) - time;
			float q1 = (3.0f * ttt) - (5.0f * tt) + 2.0f;
			float q2 = (-3.0f * ttt) + (4.0f * tt) + time;
			float q3 = ttt - tt;

			return p0 * q0 + p1 * q1 + p2 * q2 + p3 * q3;
		}

		protected override float InterpolatePoints(int left, int right, float normalTime)
		{
			float p1 = SortedKeyframes[left].m_value;
			float p2 = SortedKeyframes[right].m_value;
			return Mathf.Lerp(p1, p2, normalTime);
		}
	}
}
