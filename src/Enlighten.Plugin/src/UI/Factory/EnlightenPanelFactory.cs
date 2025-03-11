using UnityEngine;

namespace Enlighten.UI
{
	internal class EnlightenPanelFactory
	{
		private readonly GameObject m_prefab;

		public EnlightenPanelFactory(AssetBundle bundle) {
			m_prefab = bundle.LoadAsset<GameObject>("Assets/Prefabs/EnlightenPanel.prefab");
		}

		public EnlightenPanel Create(RectTransform canvas)
		{
			GameObject prefabInstance = UnityEngine.Object.Instantiate(m_prefab, canvas);
			prefabInstance.SetActive(false);

			EnlightenPanel enlightenPanel = prefabInstance.AddComponent<EnlightenPanel>();
			enlightenPanel.Initialize(canvas);
			return enlightenPanel;
		}
	}
}
