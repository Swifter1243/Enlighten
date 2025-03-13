using System;
using System.IO;
using System.Reflection;
using Enlighten.UI;
using UnityEngine;

namespace Enlighten
{
	internal static class BundleLoading
	{
		private const string ENLIGHTEN_BUNDLE_PATH = "Enlighten.enlighten_bundle";
		
		public class Assets
		{
			public GameObject m_enlightenPanelPrefab;
			public GameObject m_pointPrefab;
			public Sprite m_icon;
			public OptionPanelUI m_mainEffectOptionPanel;
		}

		public static Assets Load()
		{
			Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ENLIGHTEN_BUNDLE_PATH);
			if (resourceStream == null)
			{
				throw new NullReferenceException("Null resource stream when loading Enlighten bundle.");
			}
			
			AssetBundle bundle = AssetBundle.LoadFromStream(resourceStream);
			Assets assets = GetAssets(bundle);

			bundle.UnloadAsync(false);
			resourceStream.Dispose();
			
			return assets;
		}

		private static Assets GetAssets(AssetBundle bundle) => new Assets
		{
			m_enlightenPanelPrefab = bundle.LoadAsset<GameObject>("Assets/Prefabs/EnlightenPanel.prefab"),
			m_pointPrefab = bundle.LoadAsset<GameObject>("Assets/Prefabs/Point.prefab"),
			m_icon = bundle.LoadAsset<Sprite>("Assets/Images/icon.png"),
			m_mainEffectOptionPanel = bundle.LoadAsset<GameObject>("Assets/Prefabs/MainEffects/EffectOptionPanel.prefab").AddComponent<OptionPanelUI>(),
		};
	}
}
