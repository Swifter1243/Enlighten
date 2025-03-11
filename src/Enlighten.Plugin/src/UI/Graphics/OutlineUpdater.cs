using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class OutlineUpdater : MonoBehaviour
	{
		private readonly static int s_topRightLocal = Shader.PropertyToID("_TopRightCorner");
		private RectTransform m_rectTransform;
		private Material m_material;

		private void Awake()
		{
			m_rectTransform = GetComponent<RectTransform>();
			m_material = GetComponent<RawImage>().material;
			UpdateBorder();
		}

		public void UpdateBorder()
		{
			m_material.SetVector(s_topRightLocal, GetTopRightLocalPosition());
		}

		private Vector2 GetTopRightLocalPosition()
		{
			Rect rect = m_rectTransform.rect;
			return new Vector3(rect.width * 0.5f, rect.height * 0.5f);
		}
	}
}
