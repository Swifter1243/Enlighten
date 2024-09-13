using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class DraggableUI : MonoBehaviour, IDragHandler, IBeginDragHandler
	{
		private RectTransform targetTransform;
		private RectTransform bounds;

		public void Initialize(RectTransform targetTransform, RectTransform bounds)
		{
			this.targetTransform = targetTransform;
			this.bounds = bounds;
		}

		private Vector2 _startingMousePosition;
		private Vector2 _startingPosition;

		public void OnBeginDrag(PointerEventData eventData)
		{
			_startingMousePosition = eventData.position;
			_startingPosition = targetTransform.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector2 mouseDelta = eventData.position - _startingMousePosition;
			targetTransform.position = _startingPosition + mouseDelta;
			RectTransformHelper.ClampRectTransformWithinBounds(targetTransform, bounds);
		}
	}
}
