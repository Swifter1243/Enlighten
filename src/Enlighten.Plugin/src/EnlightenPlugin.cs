using Enlighten.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Enlighten
{
	[Plugin("Enlighten")]
	public class EnlightenPlugin
	{
		private EnlightenUI m_enlightenUI;
		private ExtensionButton m_extensionButton;

		[Init]
		private void Init()
		{
			SceneManager.sceneLoaded += SceneLoaded;

			BundleResource bundleResource = new BundleResource();
			AssetBundle bundle = bundleResource.Load();

			m_enlightenUI = new EnlightenUI(bundle);
			m_extensionButton = MakeExtensionButton(bundle);
			m_extensionButton.Click += () => m_enlightenUI.OnExtensionButtonPressed();

			bundleResource.Dispose();
		}

		private ExtensionButton MakeExtensionButton(AssetBundle bundle)
		{
			ExtensionButton button = new ExtensionButton
			{
				Tooltip = "Enlighten",
				Icon = bundle.LoadAsset<Sprite>("Assets/Images/icon.png")
			};
			ExtensionButtons.AddButton(button);
			return button;
		}

		private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			if (arg0.buildIndex == 3) // Mapping Scene
			{
				m_enlightenUI.OnMappingSceneLoaded();
			}
		}
	}
}
