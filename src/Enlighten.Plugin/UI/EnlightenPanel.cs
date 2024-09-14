using Enlighten.src.Enlighten.Plugin.UI;
using Enlighten.src.Enlighten.Plugin.UI.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class EnlightenPanel : MonoBehaviour, IFunctionalUI
	{
		private readonly Vector2 minSize = new Vector2(200, 200);

		public void Hookup(RectTransform canvas)
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			Transform insideContent = transform.Find("InsideContent");

			GameObject notch = insideContent.Find("Notch").gameObject;
			DraggableUI draggable = notch.AddComponent<DraggableUI>();
			draggable.Initialize(rectTransform, canvas);

			GameObject resizeHandle = transform.Find("ResizeHandle").gameObject;
			resizeHandle.AddComponent<ResizeHandleVisuals>();
			ResizeableUI resizeable = resizeHandle.AddComponent<ResizeableUI>();
			resizeable.Initialize(rectTransform, canvas, minSize);

			GameObject outline = transform.Find("Outline").gameObject;
			OutlineUpdater outlineUpdater = outline.AddComponent<OutlineUpdater>();
			resizeable.onResize.AddListener(outlineUpdater.UpdateBorder);
		}
	}
}
