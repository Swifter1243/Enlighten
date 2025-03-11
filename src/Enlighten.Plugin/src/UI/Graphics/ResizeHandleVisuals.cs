using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class ResizeHandleVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private const float HOVER_ANIMATION_DURATION = 1;
		private const float START_WIDTH = 0.1f;
		private const float END_WIDTH = 0.15f;
		private float m_hoverAnimationTime = 0;
		private bool m_isHovered = false;

		private const float PRESS_ANIMATION_DURATION = 0.1f;
		private readonly static Color s_unpressedColor = Color.white;
		private readonly static Color s_pressedColor = Color.gray;
		private float m_pressAnimationTime = 0;
		private bool m_isPressed = false;

		private readonly static int s_materialWidth = Shader.PropertyToID("_Width");
		private readonly static int s_materialColor = Shader.PropertyToID("_Color");
		private Material m_material;

		public void OnPointerEnter(PointerEventData eventData)
		{
			m_isHovered = true;
			m_hoverAnimationTime = 0;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			m_isHovered = false;
			m_hoverAnimationTime = 0;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			m_isPressed = true;
			m_pressAnimationTime = 0;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			m_isPressed = false;
			m_pressAnimationTime = 0;
		}

		private void Awake() {
			m_material = GetComponent<Image>().material;
			m_material.SetFloat(s_materialWidth, START_WIDTH);
			m_material.SetColor(s_materialColor, s_unpressedColor);
		}

		private void Update()
		{
			DoHoverAnimation();
			DoPressAnimation();
		}

		private void DoHoverAnimation()
		{
			m_hoverAnimationTime = Math.Min(HOVER_ANIMATION_DURATION, m_hoverAnimationTime + Time.deltaTime);

			float animationFraction = m_hoverAnimationTime / HOVER_ANIMATION_DURATION;
			animationFraction = Easing.Back.Out(animationFraction);
			float animationProgress = m_isHovered ? animationFraction : (1 - animationFraction);

			float width = Mathf.Lerp(START_WIDTH, END_WIDTH, animationProgress);
			m_material.SetFloat(s_materialWidth, width);
		}

		private void DoPressAnimation()
		{
			m_pressAnimationTime = Math.Min(PRESS_ANIMATION_DURATION, m_pressAnimationTime + Time.deltaTime);

			float animationFraction = m_pressAnimationTime / PRESS_ANIMATION_DURATION;
			float animationProgress = m_isPressed ? animationFraction : (1 - animationFraction);

			Color color = Color.Lerp(s_unpressedColor, s_pressedColor, animationProgress);
			m_material.SetColor(s_materialColor, color);
		}
	}
}
