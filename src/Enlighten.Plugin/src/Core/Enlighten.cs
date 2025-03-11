using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
using Enlighten.Core.Strip_Generator;
namespace Enlighten.Core
{
	public class Enlighten
	{
		private EventGridContainer m_eventContainer;
		public Dictionary<Modes, EnlightenMode> m_modes;

		public Enlighten(EventGridContainer eventContainer)
		{
			m_eventContainer = eventContainer;
			m_modes = GetModes();
		}

		private static BaseEvent[] SelectedEvents => SelectionController.SelectedObjects.OfType<BaseEvent>().ToArray();

		private static void Dialogue(string message)
		{
			PersistentUI.Instance.ShowDialogBox(message, null, PersistentUI.DialogBoxPresetType.Ok);
		}

		public void RunMode(EnlightenMode mode)
		{
			BaseEvent[] selectedEvents = SelectedEvents;

			if (!selectedEvents.Any())
			{
				Dialogue("No events selected");
				return;
			}

			ActionTracker actionTracker = new ActionTracker(m_eventContainer, selectedEvents);
			IEnumerable<BeatmapAction> actions = mode.Execute(m_eventContainer, selectedEvents, actionTracker);
			ActionCollectionAction actionCollection = new ActionCollectionAction(actions, false, false, "Enlighten");
			BeatmapActionContainer.AddAction(actionCollection);
		}

		private static Dictionary<Modes, EnlightenMode> GetModes() => new Dictionary<Modes, EnlightenMode>
		{
			[Modes.MainEffects] = new MainEffectsMode(),
			[Modes.StripGenerator] = new StripGeneratorMode()
		};
	}
}
