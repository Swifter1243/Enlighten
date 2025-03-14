using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Enlighten.Core
{
	public abstract class BaseParameter
	{
		public string m_name;
		public string m_description;

		public BaseParameter(string name, string description)
		{
			m_name = name;
			m_description = description;
		}
	}
	public abstract class GenericParameter<T> : BaseParameter
	{
		private readonly List<Keyframe> m_keyframes = new List<Keyframe>();
		private Keyframe[] m_sortedKeyframes;
		public Keyframe[] SortedKeyframes => m_sortedKeyframes;
		private readonly T m_defaultValue;

		public GenericParameter(T defaultValue, string name, string description) : base(name, description)
		{
			m_defaultValue = defaultValue;
			ResetToDefault();
		}

		public void ResetToDefault()
		{
			m_keyframes.Clear();
			Add(m_defaultValue, 0.25f);
			Add(m_defaultValue, 0.5f);
			Add(m_defaultValue, 0.75f);
		}

		public void SortKeyframes()
		{
			m_sortedKeyframes = m_keyframes.OrderBy(k => k.m_time).ToArray();
		}

		public struct Keyframe
		{
			public T m_value;
			public float m_time;
		}

		public void Add(T value, float time)
		{
			m_keyframes.Add(new Keyframe
			{
				m_time = time,
				m_value = value,
			});
			SortKeyframes();
		}

		public void RemoveAt(int index)
		{
			m_keyframes.RemoveAt(index);
			SortKeyframes();
		}

		public Keyframe this[int index]
		{
			get => m_keyframes[index];
			set
			{
				m_keyframes[index] = value;
				SortKeyframes();
			}
		}

		public int Count => m_keyframes.Count;

		protected abstract T InterpolatePoints(int left, int right, float normalTime);

		public T Interpolate(float time)
		{
			if (m_sortedKeyframes.Length == 0)
			{
				return default;
			}

			Keyframe lastKeyframe = m_sortedKeyframes[m_sortedKeyframes.Length - 1];
			if (lastKeyframe.m_time < time)
			{
				return lastKeyframe.m_value;
			}

			Keyframe firstKeyframe = m_sortedKeyframes[0];
			if (firstKeyframe.m_time >= time)
			{
				return firstKeyframe.m_value;
			}

			SearchIndexAtTime(time, out int leftIndex, out int rightIndex);
			Keyframe leftKeyframe = m_sortedKeyframes[leftIndex];
			Keyframe rightKeyframe = m_sortedKeyframes[rightIndex];

			float normalTime = 0;
			float divisor = rightKeyframe.m_time - leftKeyframe.m_time;
			if (divisor != 0)
			{
				normalTime = (time - leftKeyframe.m_time) / divisor;
			}

			return InterpolatePoints(leftIndex, rightIndex, normalTime);
		}

		private void SearchIndexAtTime(float time, out int left, out int right)
		{
			left = 0;
			right = m_sortedKeyframes.Length;

			while (left < right - 1)
			{
				int middle = (left + right) / 2;
				float keyframeTime = m_sortedKeyframes[middle].m_time;

				if (keyframeTime < time)
				{
					left = middle;
				}
				else
				{
					right = middle;
				}
			}
		}
	}
}
