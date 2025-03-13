using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Enlighten.UI
{
	public class ChartKeyframe : MonoBehaviour, IPointerDownHandler, IDragHandler
	{
		public event Action<Vector2> onMoved;
		public event Action onClicked;

		public void OnPointerDown(PointerEventData eventData)
		{
			onClicked?.Invoke();
		}
		public void OnDrag(PointerEventData eventData)
		{
			onMoved?.Invoke(eventData.position);
		}
	}
}
