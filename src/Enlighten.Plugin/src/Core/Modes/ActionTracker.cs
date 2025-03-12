using System.Collections.Generic;
using Beatmap.Base;
namespace Enlighten.Core
{
	public class ActionTracker
	{
		private readonly List<BeatmapObjectModifiedAction> m_modifyActions = new List<BeatmapObjectModifiedAction>();
		private readonly List<BaseObject> m_totalConflicting = new List<BaseObject>();
		private readonly List<BaseObject> m_totalAdded = new List<BaseObject>();
		private readonly EventGridContainer m_eventContainer;
		private readonly BaseEvent[] m_selectedEvents;

		public ActionTracker(EventGridContainer eventContainer, BaseEvent[] selectedEvents)
		{
			m_eventContainer = eventContainer;
			m_selectedEvents = selectedEvents;
		}

		public void Modify(BaseObject obj, BaseObject originalData)
		{
			obj.WriteCustom();
			BeatmapObjectModifiedAction modifyAction = new BeatmapObjectModifiedAction(obj, obj, originalData, "Modified with Enlighten", true);
			m_modifyActions.Add(modifyAction);
		}

		public void Add(BaseObject obj)
		{
			m_totalAdded.Add(obj);
			m_eventContainer.SpawnObject(obj, out List<BaseObject> conflicting, true, true, true);
			m_totalConflicting.AddRange(conflicting);
		}

		public IEnumerable<BeatmapAction> Finish()
		{
			List<BeatmapAction> actions = new List<BeatmapAction>(m_modifyActions);

			if (m_totalAdded.Count > 0)
			{
				actions.Add(new BeatmapObjectPlacementAction(m_totalAdded, m_totalConflicting, "Added with Enlighten"));
				m_eventContainer.DoPostObjectsSpawnedWorkflow();
			}

			if (m_modifyActions.Count > 0)
			{
				m_eventContainer.RefreshEventsAppearance(m_selectedEvents);
			}

			return actions;
		}
	}
}
