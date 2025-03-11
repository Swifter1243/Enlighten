using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.UI
{
	internal class EnlightenPanelFactory
	{
		private GameObject prefab;

		public EnlightenPanelFactory(AssetBundle bundle) {
			prefab = bundle.LoadAsset<GameObject>("Assets/Prefabs/EnlightenPanel.prefab");
		}

		public EnlightenPanel Create(RectTransform canvas)
		{
			GameObject prefabInstance = UnityEngine.Object.Instantiate(prefab, canvas);
			prefabInstance.SetActive(false);

			EnlightenPanel enlightenPanel = prefabInstance.AddComponent<EnlightenPanel>();
			enlightenPanel.Initialize(canvas);
			return enlightenPanel;
		}
	}
}
