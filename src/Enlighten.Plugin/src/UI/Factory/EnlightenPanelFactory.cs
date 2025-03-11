using UnityEngine;

namespace Enlighten.UI
{
	internal class EnlightenPanelFactory
	{
		private readonly GameObject m_prefab;

		public EnlightenPanelFactory(AssetBundle bundle) {
			m_prefab = bundle.LoadAsset<GameObject>("Assets/Prefabs/EnlightenPanel.prefab");
		}

		public EnlightenPanelUI Create(RectTransform canvas, Core.Enlighten enlighten)
		{
			GameObject prefabInstance = UnityEngine.Object.Instantiate(m_prefab, canvas);
			prefabInstance.SetActive(false);

			EnlightenPanelUI enlightenPanelUI = prefabInstance.AddComponent<EnlightenPanelUI>();
			enlightenPanelUI.Initialize(canvas, enlighten);
			return enlightenPanelUI;
		}
	}
}
