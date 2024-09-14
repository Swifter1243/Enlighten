using Enlighten.src.Enlighten.Plugin.UI;
using Enlighten.src.Enlighten.Plugin.UI.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class EnlightenPanel : MonoBehaviour
	{
		private readonly Vector2 minSize = new Vector2(200, 200);

		public void Initialize(RectTransform canvas)
		{
			// Inside Content
			RectTransform rectTransform = GetComponent<RectTransform>();
			Transform insideContent = transform.Find("InsideContent");

			// Make Draggable
			GameObject notch = insideContent.Find("Notch").gameObject;
			DraggableUI draggable = notch.AddComponent<DraggableUI>();
			draggable.Initialize(rectTransform, canvas);

			// Make Resizeable
			GameObject resizeHandle = transform.Find("ResizeHandle").gameObject;
			resizeHandle.AddComponent<ResizeHandleVisuals>();
			ResizeableUI resizeable = resizeHandle.AddComponent<ResizeableUI>();
			resizeable.Initialize(rectTransform, canvas, minSize);

			// Add Outline Logic
			GameObject outline = transform.Find("Outline").gameObject;
			OutlineUpdater outlineUpdater = outline.AddComponent<OutlineUpdater>();
			resizeable.onResize.AddListener(outlineUpdater.UpdateBorder);

			// Setup Mode Windows
			Transform modesParent = insideContent.Find("Modes");
			List<GameObject> modeGameObjects = modesParent.Cast<Transform>().Select(t => t.gameObject).ToList();
			Dropdown modeDropdown = transform.Find("ModeDropdown").GetComponent<Dropdown>();
			new GameObjectLinkedDropdown(modeDropdown, modeGameObjects, "Main Effects");
		}
	}
}
