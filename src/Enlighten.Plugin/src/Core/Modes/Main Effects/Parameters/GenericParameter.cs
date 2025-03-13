using System.Collections.Generic;
namespace Enlighten.Core
{
	public abstract class BaseParameter {}
	public abstract class GenericParameter<T> : BaseParameter
	{
		public readonly List<Keyframe> m_keyframes = new List<Keyframe>();
		private readonly T m_defaultValue;
		public string m_name;
		public string m_description;

		public GenericParameter(T defaultValue, string name, string description)
		{
			m_name = name;
			m_description = description;
			m_defaultValue = defaultValue;
			ResetToDefault();
		}

		public void ResetToDefault()
		{
			m_keyframes.Clear();
			AddPoint(m_defaultValue, 0.5f);
		}

		public struct Keyframe
		{
			public T m_value;
			public float m_time;
		}

		public void AddPoint(T value, float time)
		{
			m_keyframes.Add(new Keyframe
			{
				m_time = time,
				m_value = value,
			});
		}

		protected abstract T InterpolatePoints(int left, int right, float normalTime);

		public T Interpolate(float time)
		{
			if (m_keyframes.Count == 0)
				return default;

			Keyframe lastKeyframe = m_keyframes[m_keyframes.Count - 1];
			if (lastKeyframe.m_time < time)
			{
				return lastKeyframe.m_value;
			}

			Keyframe firstKeyframe = m_keyframes[0];
			if (lastKeyframe.m_time > time)
			{
				return firstKeyframe.m_value;
			}

			TimeIndexInfo indexInfo = SearchIndexAtTime(time);
			Keyframe leftKeyframe = m_keyframes[indexInfo.m_left];
			Keyframe rightKeyframe = m_keyframes[indexInfo.m_right];

			float normalTime = 0;
			float divisor = rightKeyframe.m_time - leftKeyframe.m_time;
			if (divisor != 0)
			{
				normalTime = (time - leftKeyframe.m_time) / divisor;
			}

			return InterpolatePoints(indexInfo.m_left, indexInfo.m_right, normalTime);
		}

		private struct TimeIndexInfo
		{
			public int m_left;
			public int m_right;
		}

		private TimeIndexInfo SearchIndexAtTime(float time)
		{
			int left = 0;
			int right = m_keyframes.Count;

			while (left < right - 1)
			{
				int middle = (left + right) / 2;
				float keyframeTime = m_keyframes[middle].m_time;

				if (keyframeTime < time)
				{
					left = middle;
				}
				else
				{
					right = middle;
				}
			}

			return new TimeIndexInfo
			{
				m_left = left,
				m_right = right
			};
		}
	}
}
