using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class EnlightenPanel : MonoBehaviour
	{
		private readonly Vector2 m_minSize = new Vector2(200, 200);

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
			resizeable.Initialize(rectTransform, canvas, m_minSize);

			// Add Outline Logic
			GameObject outline = transform.Find("Outline").gameObject;
			OutlineUpdater outlineUpdater = outline.AddComponent<OutlineUpdater>();
			resizeable.m_onResize.AddListener(outlineUpdater.UpdateBorder);

			// Setup Mode Windows
			Transform modesParent = insideContent.Find("Modes");
			List<GameObject> modeGameObjects = GetModes(modesParent).ToList();
			Dropdown modeDropdown = transform.Find("ModeDropdown").GetComponent<Dropdown>();
			new GameObjectLinkedDropdown(modeDropdown, modeGameObjects, modeGameObjects[0]);
		}

		private IEnumerable<GameObject> GetModes(Transform modesParent)
		{
			yield return AddMode<MainEffectsUI>("Main Effects");
			yield return AddMode<StripGeneratorUI>("Strip Generator");

			yield break;

			GameObject AddMode<T>(string objectName) where T : ModeUI
			{
				GameObject mode = modesParent.Find(objectName).gameObject;
				T component = mode.AddComponent<T>();
				component.Initialize();
				return mode;
			}
		}
	}
}
