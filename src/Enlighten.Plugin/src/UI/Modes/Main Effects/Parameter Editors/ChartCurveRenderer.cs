using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	public class ChartCurveRenderer : MaskableGraphic
	{
		private struct Segment
		{
			public Vector2 m_top;
			public Vector2 m_bottom;
		}
		private Segment[] m_segments;

		public void CalculateCurve(IEnumerable<Vector2> points, float thickness, float overshoot)
		{
			m_segments = CalculateSegments(points.ToArray(), thickness, overshoot).ToArray();
			SetVerticesDirty();
		}

		private IEnumerable<Segment> CalculateSegments(Vector2[] points, float thickness, float overshoot)
		{
			if (points.Length == 0)
				yield break;

			for (int i = 0; i < points.Length; i++)
			{
				Vector2 point = points[i];
				if (i == 0)
				{
					Vector2 dir = CalculateDirection(point, points[i + 1]);
					yield return FromDirection(dir, point + dir * -overshoot);
					yield return FromDirection(dir, point);
					continue;
				}
				if (i == points.Length - 1)
				{
					Vector2 dir = CalculateDirection(points[i - 1], point);
					yield return FromDirection(dir, point);
					yield return FromDirection(dir, point + dir * overshoot);
					break;
				}

				Vector2 dirBack = CalculateDirection(points[i - 1], point);
				Vector2 dirFront = CalculateDirection(point, points[i + 1]);
				Vector2 dirAvg = Vector3.Slerp(dirBack, dirFront, 0.5f);
				yield return FromDirection(dirAvg, point);
			}

			yield break;

			Vector2 CalculateDirection(Vector2 a, Vector2 b)
			{
				return (b - a).normalized;
			}
			Segment FromDirection(Vector2 d, Vector2 p)
			{
				Vector2 up = new Vector2(-d.y, d.x);
				Vector2 down = new Vector2(d.y, -d.x);

				return new Segment
				{
					m_top = p + up * thickness,
					m_bottom = p + down * thickness,
				};
			}
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();

			if (m_segments == null || m_segments.Length == 0)
				return;

			UIVertex vertex = UIVertex.simpleVert;

			for (int i = 0; i < m_segments.Length; i++)
			{
				Segment segment = m_segments[i];
				vertex.position = segment.m_top;
				vh.AddVert(vertex);
				vertex.position = segment.m_bottom;
				vh.AddVert(vertex);

				if (i >= m_segments.Length - 1)
					continue;

				int o = i * 2;
				vh.AddTriangle(0 + o, 2 + o, 1 + o);
				vh.AddTriangle(2 + o, 3 + o, 1 + o);
			}
		}
	}
}
