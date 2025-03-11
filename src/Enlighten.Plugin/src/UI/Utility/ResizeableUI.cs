using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class ResizeableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
	{
		private RectTransform targetTransform;
		private RectTransform bounds;
		private Vector2 minSize;
		private Vector2 _startCursorLocalPosition;
		private float _startOffsetMinY;
		private float _startOffsetMaxX;
		public UnityEvent onResize = new UnityEvent();

		public void Initialize(RectTransform targetTransform, RectTransform bounds, Vector2 minSize)
		{
			this.targetTransform = targetTransform;
			this.bounds = bounds;
			this.minSize = minSize;
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(bounds, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
			_startCursorLocalPosition = localCursor;
			_startOffsetMaxX = targetTransform.offsetMax.x;
			_startOffsetMinY = targetTransform.offsetMin.y;
		}

		public void OnDrag(PointerEventData eventData)
		{
			onResize.Invoke();

			RectTransformUtility.ScreenPointToLocalPointInRectangle(bounds, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);
			Vector2 localDelta = localCursor - _startCursorLocalPosition;

			Rect boundsRect = bounds.rect;
			float boundsWidth = boundsRect.width;
			float boundsHeight = boundsRect.height;

			float offsetMinY = _startOffsetMinY + localDelta.y;
			offsetMinY = Mathf.Clamp(offsetMinY, -boundsHeight, targetTransform.offsetMax.y - minSize.y);

			float offsetMaxX = _startOffsetMaxX + localDelta.x;
			offsetMaxX = Mathf.Clamp(offsetMaxX, targetTransform.offsetMin.x + minSize.x, boundsWidth);

			targetTransform.offsetMin = new Vector2(targetTransform.offsetMin.x, offsetMinY);
			targetTransform.offsetMax = new Vector2(offsetMaxX, targetTransform.offsetMax.y);
		}
	}
}
