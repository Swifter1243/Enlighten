using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class MappingSceneUI
	{
		public RectTransform sceneCanvas;
		public EnlightenPanel enlightenPanel;

		public MappingSceneUI(MappingSceneUIFactory factory)
		{
			MapEditorUI mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			sceneCanvas = mapEditorUI.MainUIGroup[5].transform.GetComponent<RectTransform>();
			enlightenPanel = factory.enlightenPanelFactory.Create(sceneCanvas);
		}
	}
}
