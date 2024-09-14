using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin.UI.Graphics
{
	internal class ResizeHandleVisuals : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private static readonly float HOVER_ANIMATION_DURATION = 1;
		private static readonly float START_WIDTH = 0.1f;
		private static readonly float END_WIDTH = 0.15f;
		private float hoverAnimationTime = 0;
		private bool isHovered = false;

		private static readonly float PRESS_ANIMATION_DURATION = 0.1f;
		private static readonly Color UNPRESSED_COLOR = Color.white;
		private static readonly Color PRESSED_COLOR = Color.gray;
		private float pressAnimationTime = 0;
		private bool isPressed = false;

		private static readonly int materialWidth = Shader.PropertyToID("_Width");
		private static readonly int materialColor = Shader.PropertyToID("_Color");
		private Material material;

		public void OnPointerEnter(PointerEventData eventData)
		{
			isHovered = true;
			hoverAnimationTime = 0;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			isHovered = false;
			hoverAnimationTime = 0;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			isPressed = true;
			pressAnimationTime = 0;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			isPressed = false;
			pressAnimationTime = 0;
		}

		private void Awake() {
			material = GetComponent<Image>().material;
			material.SetFloat(materialWidth, START_WIDTH);
			material.SetColor(materialColor, UNPRESSED_COLOR);
		}

		private void Update()
		{
			DoHoverAnimation();
			DoPressAnimation();
		}

		private void DoHoverAnimation()
		{
			hoverAnimationTime = Math.Min(HOVER_ANIMATION_DURATION, hoverAnimationTime + Time.deltaTime);

			float animationFraction = hoverAnimationTime / HOVER_ANIMATION_DURATION;
			animationFraction = Easing.Back.Out(animationFraction);
			float animationProgress = isHovered ? animationFraction : (1 - animationFraction);

			float width = Mathf.Lerp(START_WIDTH, END_WIDTH, animationProgress);
			material.SetFloat(materialWidth, width);
		}

		private void DoPressAnimation()
		{
			pressAnimationTime = Math.Min(PRESS_ANIMATION_DURATION, pressAnimationTime + Time.deltaTime);

			float animationFraction = pressAnimationTime / PRESS_ANIMATION_DURATION;
			float animationProgress = isPressed ? animationFraction : (1 - animationFraction);

			Color color = Color.Lerp(UNPRESSED_COLOR, PRESSED_COLOR, animationProgress);
			material.SetColor(materialColor, color);
		}
	}
}
