using System;
using System.Collections.Generic;
using System.Linq;
using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal abstract class ChartParameterEditor<T> : MonoBehaviour
	{
		private Transform m_pointsParent;
		private BundleLoading.Assets m_assets;
		private GenericParameter<T> m_parameter;
		private ChartKeyframe<T>[] m_keyframes;

		public event Action<int> onKeyframeMoved;
		public event Action<int> onKeyframeSelected;

		public void Initialize(BundleLoading.Assets assets)
		{
			m_assets = assets;
			m_pointsParent = transform.Find("Points");
		}

		public void OpenParameter(GenericParameter<T> parameter)
		{
			m_parameter = parameter;
			m_keyframes = GetKeyframes().ToArray();
		}

		private IEnumerable<ChartKeyframe<T>> GetKeyframes()
		{
			for (int i = 0; i < m_parameter.m_keyframes.Count; i++)
			{
				ChartKeyframe<T> keyframe = Instantiate(m_assets.m_pointPrefab, m_pointsParent).AddComponent<ChartKeyframe<T>>();
				int tempIndex = i;
				keyframe.onClicked += () => onKeyframeSelected?.Invoke(tempIndex);
				keyframe.onMoved += position => OnKeyframeMove(tempIndex, position);
				yield return keyframe;
			}
		}

		private Vector2 WorldToChartPosition(Vector2 position)
		{
			// TODO
			return Vector2.zero;
		}

		private GenericParameter<T>.Keyframe ChartPositionToKeyframeValues(Vector2 position)
		{
			return new GenericParameter<T>.Keyframe
			{
				m_time = position.x,
				m_value = ChartPositionYToValue(position.y)
			};
		}

		protected abstract T ChartPositionYToValue(float y);

		private void OnKeyframeMove(int index, Vector2 position)
		{
			Vector2 chartPosition = WorldToChartPosition(position);
			chartPosition = HandleKeyframeMove(chartPosition);
			GenericParameter<T>.Keyframe keyframe = ChartPositionToKeyframeValues(chartPosition);
			m_parameter.m_keyframes[index] = keyframe;
			onKeyframeMoved?.Invoke(index);
		}

		protected abstract Vector2 HandleKeyframeMove(Vector2 chartPosition);

		public void RedrawPoints()
		{
			foreach (Transform child in m_pointsParent)
			{
				Destroy(child.gameObject);
			}
			m_keyframes = GetKeyframes().ToArray();
		}

		public void RedrawCompletely()
		{
			RedrawPoints();
			RedrawCurves();
		}

		protected void RedrawCurves()
		{
			// TODO
		}
	}
}
