using UnityEngine;

namespace Enlighten.UI
{

	internal class EnlightenUI
	{
		private MappingSceneUIFactory mappingSceneUIFactory;
		private MappingSceneUI mappingSceneUI;

		public EnlightenUI(AssetBundle bundle)
		{
			mappingSceneUIFactory = new MappingSceneUIFactory(bundle);
		}

		public void OnMappingSceneLoaded()
		{
			mappingSceneUI = new MappingSceneUI(mappingSceneUIFactory);
		}

		public void OnExtensionButtonPressed()
		{
			GameObject enlightenPanelObj = mappingSceneUI.m_enlightenPanel.gameObject;
			enlightenPanelObj.SetActive(!enlightenPanelObj.activeSelf);
		}
	}
}
