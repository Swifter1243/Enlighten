using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class EnlightenPanelFactory
	{
		private GameObject prefab;

		public EnlightenPanelFactory(AssetBundle bundle) {
			prefab = bundle.LoadAsset<GameObject>("Assets/EnlightenPanel.prefab");
		}

		public EnlightenPanel Create(Transform parent)
		{
			GameObject prefabInstance = UnityEngine.Object.Instantiate(prefab, parent);
			prefabInstance.SetActive(false);

			EnlightenPanel enlightenPanel = prefabInstance.AddComponent<EnlightenPanel>();
			enlightenPanel.Hookup();
			return enlightenPanel;
		}
	}
}
