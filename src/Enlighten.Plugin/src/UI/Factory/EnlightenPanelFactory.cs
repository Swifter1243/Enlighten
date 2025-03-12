using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class EnlightenPanelFactory
	{
		private readonly BundleLoading.Assets m_assets;

		public EnlightenPanelFactory(BundleLoading.Assets assets) {
			m_assets = assets;
		}

		public EnlightenPanelUI Create(RectTransform canvas, Core.Enlighten enlighten)
		{
			GameObject prefabInstance = Object.Instantiate(m_assets.m_enlightenPanelPrefab, canvas);
			prefabInstance.SetActive(false);

			EnlightenPanelUI enlightenPanelUI = prefabInstance.AddComponent<EnlightenPanelUI>();
			enlightenPanelUI.Initialize(canvas, enlighten, m_assets);
			return enlightenPanelUI;
		}
	}
}
