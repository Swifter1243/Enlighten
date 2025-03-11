using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Enlighten.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Enlighten
{
	[Plugin("Enlighten")]
	public class EnlightenPlugin
	{
		private EnlightenUI enlightenUI;
		private ExtensionButton extensionButton;

		[Init]
		private void Init()
		{
			SceneManager.sceneLoaded += SceneLoaded;

			BundleResource bundleResource = new BundleResource();
			AssetBundle bundle = bundleResource.Load();

			enlightenUI = new EnlightenUI(bundle);
			extensionButton = MakeExtensionButton(bundle);
			extensionButton.Click += () => enlightenUI.OnExtensionButtonPressed();

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
				enlightenUI.OnMappingSceneLoaded();
			}
		}
	}
}
