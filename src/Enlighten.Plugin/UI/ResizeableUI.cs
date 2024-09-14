using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class ResizeableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
	{
		private RectTransform targetTransform;
		private RectTransform bounds;
		private Vector2 minSize;
		private Vector2 _startCursorLocalPosition;
		private float _beginRight;
		private float _beginBottom;

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
			_beginRight = targetTransform.GetRight();
			_beginBottom = targetTransform.GetBottom();
		}

		public void OnDrag(PointerEventData eventData)
		{
			// Convert the initial cursor position to local canvas space as well
			RectTransformUtility.ScreenPointToLocalPointInRectangle(bounds, eventData.position, eventData.pressEventCamera, out Vector2 localCursor);

			// Calculate the mouse movement delta in local canvas space
			Vector2 localDelta = localCursor - _startCursorLocalPosition;

			Rect boundsRect = bounds.rect;
			float boundsWidth = boundsRect.width;
			float boundsHeight = boundsRect.height;

			// Adjust the right and bottom values based on the local movement
			float newRight = _beginRight - localDelta.x;
			float rightAtLeft = boundsWidth - targetTransform.GetLeft();
			//newRight = Mathf.Clamp(newRight, 0, rightAtLeft - minSize.x);

			float newBottom = _beginBottom + localDelta.y;
			float bottomAtTop = boundsHeight - targetTransform.GetTop();

			targetTransform.SetRight(newRight);
			targetTransform.SetBottom(newBottom);
		}
	}
}
