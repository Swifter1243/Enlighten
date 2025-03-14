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
		private ChartCurveRenderer m_curveRenderer;
		private BundleLoading.Assets m_assets;
		private GenericParameter<T> m_parameter;
		private ChartKeyframe[] m_keyframes;

		protected float m_chartLowBound = 0f;
		protected float m_chartHighBound = 1f;

		public void Initialize(BundleLoading.Assets assets)
		{
			m_assets = assets;

			Transform graph = transform.Find("Graph");
			m_pointsParent = graph.Find("Points").GetComponent<RectTransform>();
			m_curveRenderer = graph.Find("Curves").gameObject.AddComponent<ChartCurveRenderer>();
		}

		public void OpenParameter(GenericParameter<T> parameter)
		{
			m_parameter = parameter;
			RedrawCompletely();
		}

		private IEnumerable<ChartKeyframe> GetKeyframes()
		{
			for (int i = 0; i < m_parameter.Count; i++)
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
			for (int i = 0; i < m_parameter.Count; i++)
			{
				GenericParameter<T>.Keyframe data = m_parameter[i];
				ChartKeyframe obj = m_keyframes[i];
				float chartX = data.m_time;
				float chartY = ValueToChartYPosition(data.m_value);
				Vector2 screenPos = ChartToLocalPosition(new Vector2(chartX, chartY));
				obj.transform.localPosition = screenPos;
			}
		}

		private Vector2 LocalToChartPosition(Vector2 localPosition)
		{
			Vector2 chartPosition = localPosition - m_pointsParent.rect.min;
			chartPosition /= m_pointsParent.rect.size;
			chartPosition.y += m_chartLowBound;
			chartPosition.y *= m_chartHighBound - m_chartLowBound;
			return chartPosition;
		}

		private Vector2 ChartToLocalPosition(Vector2 chartPosition)
		{
			Vector2 localPosition = chartPosition;
			localPosition.y /= m_chartHighBound - m_chartLowBound;
			localPosition.y -= m_chartLowBound;
			localPosition *= m_pointsParent.rect.size;
			return m_pointsParent.rect.min + localPosition;
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
			Vector2 localPosition = m_pointsParent.InverseTransformPoint(screenPosition);
			localPosition = m_pointsParent.ClampPointInside(localPosition);
			m_keyframes[index].transform.localPosition = localPosition;
			Vector2 chartPosition = LocalToChartPosition(localPosition);
			GenericParameter<T>.Keyframe keyframe = ChartPositionToKeyframeValues(chartPosition);
			m_parameter[index] = keyframe;
			m_onKeyframeChanged.Invoke(index);
			RedrawCurves();
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
			m_curveRenderer.CalculateCurve(FullResolutionPoints());
		}

		private IEnumerable<Vector2> FullResolutionPoints()
		{
			GenericParameter<T>.Keyframe[] keyframes = m_parameter.SortedKeyframes;

			const int ITERATIONS = 100;
			for (int i = 0; i <= ITERATIONS; i++)
			{
				float t = i / (float)ITERATIONS;
				yield return SampleTime(t);
			}

			Vector2 SampleTime(float t)
			{
				T value = m_parameter.Interpolate(t);
				float y = ValueToChartYPosition(value);
				Vector2 chartPosition = new Vector2(t, y);
				return ChartToLocalPosition(chartPosition);
			}
		}

		private IEnumerable<Vector2> GetCurveLocalPoints()
		{
			GenericParameter<T>.Keyframe[] keyframes = m_parameter.SortedKeyframes;

			switch (keyframes.Length)
			{
			case 0:
				yield break;
			case 1:
				T value = keyframes[0].m_value;
				float y = ValueToChartYPosition(value);
				yield return ChartToLocalPosition(new Vector2(0, y));
				yield return ChartToLocalPosition(new Vector2(1, y));
				yield break;
			}

			if (keyframes[0].m_time > 0)
				yield return SampleTime(0);

			for (int i = 0; i < keyframes.Length; i++)
			{
				float a = keyframes[i].m_time;
				yield return SampleTime(a);

				if (i == keyframes.Length - 1)
					continue;

				float b = keyframes[i + 1].m_time;

				const int RESOLUTION = 5;
				for (int j = 1; j < RESOLUTION; j++)
				{
					float f = j / (float)RESOLUTION;
					float t = Mathf.Lerp(a, b, f);
					yield return SampleTime(t);
				}
			}

			if (keyframes[keyframes.Length - 1].m_time < 1)
				yield return SampleTime(1);

			yield break;

			Vector2 SampleTime(float t)
			{
				T value = m_parameter.Interpolate(t);
				float y = ValueToChartYPosition(value);
				Vector2 chartPosition = new Vector2(t, y);
				return ChartToLocalPosition(chartPosition);
			}
		}
	}
}
