using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Enlighten.UI
{
	internal class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
	{
		private RectTransform m_targetTransform;
		private RectTransform m_bounds;
		private Vector2 m_startingMousePosition;
		private Vector2 m_startingPosition;
		public UnityEvent m_onDrag = new UnityEvent();

		public void Initialize(RectTransform targetTransform, RectTransform bounds)
		{
			this.m_targetTransform = targetTransform;
			this.m_bounds = bounds;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			m_startingMousePosition = eventData.position;
			m_startingPosition = m_targetTransform.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			m_onDrag.Invoke();

			Vector2 mouseDelta = eventData.position - m_startingMousePosition;
			m_targetTransform.position = m_startingPosition + mouseDelta;
			m_targetTransform.ClampWithinBounds(m_bounds);
		}
	}
}
