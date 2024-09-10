using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enlighten.src.Enlighten.Plugin.UI
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
			GameObject enlightenPanelObj = mappingSceneUI.enlightenPanel.gameObject;
			enlightenPanelObj.SetActive(!enlightenPanelObj.activeSelf);
		}
	}
}
