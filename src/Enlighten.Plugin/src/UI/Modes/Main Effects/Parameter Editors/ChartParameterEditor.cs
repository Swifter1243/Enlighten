using System;
using System.Collections.Generic;
using System.Linq;
using Enlighten.Core;
using UnityEngine;
using UnityEngine.Events;
namespace Enlighten.UI
{
	internal abstract class ChartParameterEditor<T> : BaseParameterEditor
	{
		private RectTransform m_pointsParent;
		private BundleLoading.Assets m_assets;
		private GenericParameter<T> m_parameter;
		private ChartKeyframe[] m_keyframes;

		public void Initialize(BundleLoading.Assets assets)
		{
			m_assets = assets;
			m_pointsParent = transform.Find("Points").GetComponent<RectTransform>();
		}

		public void OpenParameter(GenericParameter<T> parameter)
		{
			m_parameter = parameter;
			RedrawCompletely();
		}

		private IEnumerable<ChartKeyframe> GetKeyframes()
		{
			for (int i = 0; i < m_parameter.m_keyframes.Count; i++)
			{
				ChartKeyframe keyframe = Instantiate(m_assets.m_pointPrefab, m_pointsParent).AddComponent<ChartKeyframe>();
				int tempIndex = i;
				keyframe.onClicked += () => m_onKeyframeSelected.Invoke(tempIndex);
				keyframe.onMoved += position => OnKeyframeMove(tempIndex, position);
				yield return keyframe;
			}
		}

		protected void UpdateKeyframePositions()
		{
			for (int i = 0; i < m_parameter.m_keyframes.Count; i++)
			{
				GenericParameter<T>.Keyframe data = m_parameter.m_keyframes[i];
				ChartKeyframe obj = m_keyframes[i];
				float chartX = data.m_time;
				float chartY = ValueToChartYPosition(data.m_value);
				Vector2 screenPos = ChartToScreenPosition(new Vector2(chartX, chartY));
				obj.transform.position = screenPos;
			}
		}

		private Vector2 ScreenToChartPosition(Vector2 position)
		{
			// TODO
			return Vector2.zero;
		}

		private Vector2 ChartToScreenPosition(Vector2 position)
		{
			// TODO
			return Vector2.zero;
		}

		private GenericParameter<T>.Keyframe ChartPositionToKeyframeValues(Vector2 position)
		{
			return new GenericParameter<T>.Keyframe
			{
				m_time = position.x,
				m_value = ChartYPositionToValue(position.y)
			};
		}

		protected abstract T ChartYPositionToValue(float y);
		protected abstract float ValueToChartYPosition(T value);

		private void OnKeyframeMove(int index, Vector2 screenPosition)
		{
			screenPosition = m_pointsParent.ClampPointInside(screenPosition);
			Vector2 chartPosition = ScreenToChartPosition(screenPosition);
			GenericParameter<T>.Keyframe keyframe = ChartPositionToKeyframeValues(chartPosition);
			m_parameter.m_keyframes[index] = keyframe;
			m_onKeyframeChanged.Invoke(index);
		}

		public void RedrawKeyframes()
		{
			foreach (Transform child in m_pointsParent)
			{
				Destroy(child.gameObject);
			}
			m_keyframes = GetKeyframes().ToArray();
			UpdateKeyframePositions();
		}

		public override void RedrawCompletely()
		{
			RedrawKeyframes();
			RedrawCurves();
		}

		protected void RedrawCurves()
		{
			// TODO
		}
	}
}
