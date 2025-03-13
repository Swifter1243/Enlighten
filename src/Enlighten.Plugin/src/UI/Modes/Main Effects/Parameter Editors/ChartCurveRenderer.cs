using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	public class ChartCurveRenderer : Graphic
	{
		private struct Segment
		{
			public Vector2 m_top;
			public Vector2 m_bottom;
		}
		private Segment[] m_segments;

		public void CalculateCurve(IEnumerable<Vector2> points)
		{
			m_segments = CalculateSegments(points.ToArray(), 1, 10).ToArray();
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

			for (int i = 0; i < m_segments.Length - 1; i++)
			{
				Segment a = m_segments[i];
				Segment b = m_segments[i + 1];

				UIVertex vertex = UIVertex.simpleVert;

				vertex.position = a.m_top;
				vh.AddVert(vertex);
				vertex.position = b.m_top;
				vh.AddVert(vertex);
				vertex.position = b.m_bottom;
				vh.AddVert(vertex);
				vertex.position = a.m_bottom;
				vh.AddVert(vertex);

				int offset = i * 4;
				vh.AddTriangle(0 + offset, 1 + offset, 2 + offset);
				vh.AddTriangle(2 + offset, 3 + offset, 0 + offset);
			}
		}
	}
}
