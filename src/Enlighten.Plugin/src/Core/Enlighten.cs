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

		private static IEnumerable<BaseEvent> SelectedEvents => SelectionController.SelectedObjects.OfType<BaseEvent>();

		private static void Dialogue(string message)
		{
			PersistentUI.Instance.ShowDialogBox(message, null, PersistentUI.DialogBoxPresetType.Ok);
		}

		public void RunMode(EnlightenMode mode)
		{
			IEnumerable<BaseEvent> selectedEvents = SelectedEvents;

			if (!selectedEvents.Any())
			{
				Dialogue("No events selected");
				return;
			}

			IEnumerable<BeatmapAction> actions = mode.Execute(m_eventContainer);
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
