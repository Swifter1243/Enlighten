using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin
{
	internal class EnlightenPanelFactory
	{
		private EnlightenPanel prefab;

		public EnlightenPanelFactory(AssetBundle bundle) {
			prefab = MakePrefab(bundle);
		}

		private EnlightenPanel MakePrefab(AssetBundle bundle)
		{
			GameObject panelPrefab = bundle.LoadAsset<GameObject>("Assets/EnlightenPanel.prefab");
			return panelPrefab.AddComponent<EnlightenPanel>();
		}

		public EnlightenPanel Create(Transform parent)
		{
			return UnityEngine.Object.Instantiate(prefab, parent);
		}
	}
}
