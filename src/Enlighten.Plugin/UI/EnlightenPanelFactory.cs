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
		private GameObject prefab;

		public EnlightenPanelFactory(AssetBundle bundle) {
			prefab = bundle.LoadAsset<GameObject>("Assets/EnlightenPanel.prefab");
		}

		public EnlightenPanel Create(Transform parent)
		{
			return UnityEngine.Object.Instantiate(prefab, parent).AddComponent<EnlightenPanel>();
		}
	}
}
