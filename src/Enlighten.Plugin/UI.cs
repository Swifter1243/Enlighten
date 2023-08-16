using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using UnityEngine;
using Beatmap.Base;
using System.IO;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin
{
	public class UI
	{
		public Enlighten plugin;
		public GameObject panelAsset;
		public Transform canvas;
		public EnlightenPanel panel;

		public UI(Enlighten plugin)
		{
			this.plugin = plugin;

			// Extension Button
			var button = new ExtensionButton();
			button.Tooltip = "Enlighten";
			button.Click = OnPress;
			ExtensionButtons.AddButton(button);

			// UI Setup
			panelAsset = plugin.bundle.LoadAsset<GameObject>("Assets/EnlightenPanel.prefab");
			var enlightenPanel = panelAsset.AddComponent<EnlightenPanel>();
			enlightenPanel.run = panelAsset.transform.Find("Run").GetComponent<Button>();
		}

		public void OnLoad()
		{
			var mapEditorUI = UnityEngine.Object.FindObjectOfType<MapEditorUI>();
			canvas = mapEditorUI.MainUIGroup[5].transform;

			panel = UnityEngine.Object.Instantiate(panelAsset, canvas).GetComponent<EnlightenPanel>();
			panel.transform.localScale = new Vector3(1, 1, 1);
			panel.Initialize();
			panel.run.onClick.AddListener(Test);
			panel.gameObject.SetActive(false);
		}

		private void Test()
		{
			panel.WriteToValues(panel.currVals);
			
			foreach (var thing in panel.currVals)
			{
				Debug.Log(thing.Key + ": " + thing.Value);
			}
		}

		private void OnPress()
		{
			panel.gameObject.SetActive(!panel.isActiveAndEnabled);
		}

		private void TestProcess()
		{
			var actions = new List<BeatmapAction>();
			var events = plugin.events.LoadedObjects.Cast<BaseEvent>();

			foreach (var light in events)
			{
				if (light.CustomColor != null)
				{
					var original = (BaseObject)light.Clone();
					light.CustomColor *= 3;
					light.WriteCustom();

					var action = new BeatmapObjectModifiedAction(light, light, original, "Modified with Enlighten.", true);
					actions.Add(action);
				}
			}

			var allActions = new ActionCollectionAction(actions, false, false, "Modified with Enlighten.");
			BeatmapActionContainer.AddAction(allActions);

			plugin.events.RefreshEventsAppearance(events);
		}
	}
}
