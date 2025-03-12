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
		private BundleLoading.Assets m_assets;

		public void Initialize(RectTransform canvas, Core.Enlighten enlighten, BundleLoading.Assets assets)
		{
			m_assets = assets;
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
			yield return AddMode("Main Effects", Modes.MainEffects);
			yield return AddMode("Strip Generator", Modes.StripGenerator);

			yield break;

			ModeUI AddMode(string objectName, Modes mode)
			{
				GameObject go = modesParent.Find(objectName).gameObject;
				ModeUI component = go.AddComponent<ModeUI>();
				EnlightenMode enlightenMode = m_enlighten.m_modes[mode];
				component.Initialize(enlightenMode);
				MakeUI(go, enlightenMode);
				return component;
			}
		}

		private void MakeUI(GameObject go, EnlightenMode enlightenMode)
		{
			switch (enlightenMode)
			{
			case MainEffectsMode mainEffectsMode:
				go.AddComponent<MainEffectsUI>().Initialize(mainEffectsMode, m_assets);
				break;
			case StripGeneratorMode stripGeneratorMode:
				go.AddComponent<StripGeneratorUI>().Initialize(stripGeneratorMode);
				break;
			}
		}

		private void OnRun()
		{
			m_enlighten.RunMode(m_modes.ActiveObject.m_enlightenMode);
		}
	}
}
