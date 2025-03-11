using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Enlighten.UI
{
	internal class ResizeableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
	{
		private RectTransform m_targetTransform;
		private RectTransform m_bounds;
		private Vector2 m_minSize;
		private Vector2 m_startCursorLocalPosition;
		private float m_startOffsetMinY;
		private float m_startOffsetMaxX;
		public UnityEvent m_onResize = new UnityEvent();

		public void Initialize(RectTransform targetTransform, RectTransform bounds, Vector2 minSize)
		{
			this.m_targetTransform = targetTransform;
			this.m_bounds = bounds;
			this.m_minSize = minSize;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(m_bounds, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
			m_startCursorLocalPosition = localCursor;
			m_startOffsetMaxX = m_targetTransform.offsetMax.x;
			m_startOffsetMinY = m_targetTransform.offsetMin.y;
		}

		public void OnDrag(PointerEventData eventData)
		{
			m_onResize.Invoke();

			RectTransformUtility.ScreenPointToLocalPointInRectangle(m_bounds, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
			Vector2 localDelta = localCursor - m_startCursorLocalPosition;

			Rect boundsRect = m_bounds.rect;
			float boundsWidth = boundsRect.width;
			float boundsHeight = boundsRect.height;

			float offsetMinY = m_startOffsetMinY + localDelta.y;
			offsetMinY = Mathf.Clamp(offsetMinY, -boundsHeight, m_targetTransform.offsetMax.y - m_minSize.y);

			float offsetMaxX = m_startOffsetMaxX + localDelta.x;
			offsetMaxX = Mathf.Clamp(offsetMaxX, m_targetTransform.offsetMin.x + m_minSize.x, boundsWidth);

			m_targetTransform.offsetMin = new Vector2(m_targetTransform.offsetMin.x, offsetMinY);
			m_targetTransform.offsetMax = new Vector2(offsetMaxX, m_targetTransform.offsetMax.y);
		}
	}
}
