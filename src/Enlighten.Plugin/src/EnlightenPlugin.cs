﻿using Enlighten.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten
{
	[Plugin("Enlighten")]
	public class EnlightenPlugin
	{
		private EnlightenUI m_enlightenUI;
		private ExtensionButton m_extensionButton;
		private Core.Enlighten m_enlighten;

		[Init]
		private void Init()
		{
			SceneManager.sceneLoaded += SceneLoaded;

			BundleLoading.Assets assets = BundleLoading.Load();

			m_enlightenUI = new EnlightenUI(ref assets);
			m_extensionButton = MakeExtensionButton(ref assets);
			m_extensionButton.Click += () => m_enlightenUI.OnExtensionButtonPressed();
		}

		private static ExtensionButton MakeExtensionButton(ref BundleLoading.Assets assets)
		{
			ExtensionButton button = new ExtensionButton
			{
				Tooltip = "Enlighten",
				Icon = assets.m_icon
			};
			ExtensionButtons.AddButton(button);
			return button;
		}

		private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			switch (arg0.buildIndex)
			{
			case 3:
				OnMappingSceneLoaded();
				break;
			}
		}

		private void OnMappingSceneLoaded()
		{
			EventGridContainer events = Object.FindObjectOfType<EventGridContainer>();
			m_enlighten = new Core.Enlighten(events);
			m_enlightenUI.OnMappingSceneLoaded(m_enlighten);
		}
	}
}
