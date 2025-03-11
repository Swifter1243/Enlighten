using UnityEngine;

namespace Enlighten.UI
{
	internal class MappingSceneUI
	{
		public readonly RectTransform m_sceneCanvas;
		public readonly EnlightenPanelUI m_enlightenPanelUI;

		public MappingSceneUI(MappingSceneUIFactory factory, Core.Enlighten enlighten)
		{
			MapEditorUI mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			m_sceneCanvas = mapEditorUI.MainUIGroup[5].transform.GetComponent<RectTransform>();
			m_enlightenPanelUI = factory.m_enlightenPanelFactory.Create(m_sceneCanvas, enlighten);
		}
	}
}
