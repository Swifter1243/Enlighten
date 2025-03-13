using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Enlighten.UI
{
	public abstract class BaseParameterUI : MonoBehaviour, IPointerDownHandler
	{
		protected int m_selectedKeyframeIndex;

		private void Awake()
		{
			m_onUIChanged.AddListener(m_onInteracted.Invoke);
		}

		public abstract void UpdateUI();
		public abstract void SortParameter();
		public UnityEvent m_onUIChanged = new UnityEvent();
		public UnityEvent m_onInteracted = new UnityEvent();

		public void SetActiveKeyframeIndex(int index)
		{
			m_selectedKeyframeIndex = index;
			UpdateUI();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			m_onInteracted.Invoke();
		}
	}
}
