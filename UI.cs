using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using UnityEngine;
using Beatmap.Base;
using System.IO;

namespace Enlighten
{
	public class UI
	{
		public Enlighten plugin;
		public GameObject panelAsset;
		public Transform canvas;
		public GameObject panel;

		public UI(Enlighten plugin)
		{
			panelAsset = plugin.bundle.LoadAsset<GameObject>("Assets/Enlighten.prefab");
			this.plugin = plugin;

			var button = new ExtensionButton();
			button.Tooltip = "Enlighten";
			button.Click = OnPress;

			ExtensionButtons.AddButton(button);
		}

		private void OnPress()
		{
			if (panel == null)
			{
				panel = UnityEngine.Object.Instantiate(panelAsset, canvas);
				panel.transform.localScale = new Vector3(1, 1, 1);
			}
			else
			{
				UnityEngine.Object.Destroy(panel);
				panel = null;
			}
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
