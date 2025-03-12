using UnityEngine;

namespace Enlighten.UI
{

	internal class EnlightenUI
	{
		private readonly MappingSceneUIFactory m_mappingSceneUIFactory;
		private MappingSceneUI m_mappingSceneUI;

		public EnlightenUI(ref BundleLoading.Assets assets)
		{
			m_mappingSceneUIFactory = new MappingSceneUIFactory(ref assets);
		}

		public void OnMappingSceneLoaded(Core.Enlighten enlighten)
		{
			m_mappingSceneUI = new MappingSceneUI(m_mappingSceneUIFactory, enlighten);
		}

		public void OnExtensionButtonPressed()
		{
			GameObject enlightenPanelObj = m_mappingSceneUI.m_enlightenPanelUI.gameObject;
			enlightenPanelObj.SetActive(!enlightenPanelObj.activeSelf);
		}
	}
}
