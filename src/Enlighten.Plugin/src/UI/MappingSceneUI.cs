using UnityEngine;

namespace Enlighten.UI
{
	internal class MappingSceneUI
	{
		public readonly RectTransform m_sceneCanvas;
		public readonly EnlightenPanel m_enlightenPanel;

		public MappingSceneUI(MappingSceneUIFactory factory)
		{
			MapEditorUI mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			m_sceneCanvas = mapEditorUI.MainUIGroup[5].transform.GetComponent<RectTransform>();
			m_enlightenPanel = factory.m_enlightenPanelFactory.Create(m_sceneCanvas);
		}
	}
}
