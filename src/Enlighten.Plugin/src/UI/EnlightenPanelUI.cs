using System.Collections.Generic;
using System.Linq;
using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class EnlightenPanelUI : MonoBehaviour
	{
		private readonly Vector2 m_minSize = new Vector2(200, 200);
		private RectTransform m_rt;
		private Core.Enlighten m_enlighten;
		private LinkedDropdown<ModeUI> m_modes;

		public void Initialize(RectTransform canvas, Core.Enlighten enlighten)
		{
			m_enlighten = enlighten;
			m_rt = GetComponent<RectTransform>();
			Transform insideContent = transform.Find("InsideContent");

			ResizeableUI resizeable = SetupResizing(canvas);
			SetupDragging(canvas, insideContent);
			SetupOutline(resizeable);
			SetupModes(insideContent);
		}

		private void SetupOutline(ResizeableUI resizeable)
		{
			GameObject outline = transform.Find("Outline").gameObject;
			OutlineUpdater outlineUpdater = outline.AddComponent<OutlineUpdater>();
			resizeable.m_onResize.AddListener(outlineUpdater.UpdateBorder);
		}

		private ResizeableUI SetupResizing(RectTransform canvas)
		{
			GameObject resizeHandle = transform.Find("ResizeHandle").gameObject;
			resizeHandle.AddComponent<ResizeHandleVisuals>();
			ResizeableUI resizeable = resizeHandle.AddComponent<ResizeableUI>();
			resizeable.Initialize(m_rt, canvas, m_minSize);
			return resizeable;
		}

		private void SetupDragging(RectTransform canvas, Transform insideContent)
		{
			GameObject notch = insideContent.Find("Notch").gameObject;
			DraggableUI draggable = notch.AddComponent<DraggableUI>();
			draggable.Initialize(m_rt, canvas);
		}

		private void SetupModes(Transform insideContent)
		{
			Transform modesParent = insideContent.Find("Modes");
			List<ModeUI> modeGameObjects = GetModes(modesParent).ToList();
			Dropdown modeDropdown = transform.Find("ModeDropdown").GetComponent<Dropdown>();
			m_modes = new LinkedDropdown<ModeUI>(modeDropdown, modeGameObjects, modeGameObjects[0]);
		}

		private IEnumerable<ModeUI> GetModes(Transform modesParent)
		{
			yield return AddMode<MainEffectsUI>("Main Effects", Modes.MainEffects);
			yield return AddMode<StripGeneratorUI>("Strip Generator", Modes.StripGenerator);

			yield break;

			ModeUI AddMode<T>(string objectName, Modes mode) where T : ModeUI
			{
				GameObject go = modesParent.Find(objectName).gameObject;
				T component = go.AddComponent<T>();
				EnlightenMode enlightenMode = m_enlighten.m_modes[mode];
				component.Initialize(enlightenMode);
				return component;
			}
		}
	}
}
