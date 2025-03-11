using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class OutlineUpdater : MonoBehaviour
	{
		private static readonly int TopRightLocal = Shader.PropertyToID("_TopRightCorner");
		private RectTransform rectTransform;
		private Material material;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			material = GetComponent<RawImage>().material;
			UpdateBorder();
		}

		public void UpdateBorder()
		{
			material.SetVector(TopRightLocal, GetTopRightLocalPosition());
		}

		Vector2 GetTopRightLocalPosition()
		{
			Rect rect = rectTransform.rect;
			return new Vector3(rect.width * 0.5f, rect.height * 0.5f);
		}
	}
}
