using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Beatmap.Base;

namespace Enlighten
{
	class UI
	{
		private Enlighten plugin;

		public UI(Enlighten plugin)
		{
			this.plugin = plugin;

			var button = new ExtensionButton();
			button.Tooltip = "Enlighten";
			button.Click = OnPress;

			ExtensionButtons.AddButton(button);
		}

		private void OnPress()
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
