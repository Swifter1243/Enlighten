using Beatmap.Base;
using UnityEngine;
namespace Enlighten.Core
{
	public abstract class SimpleColorEffect : Effect
	{
		protected abstract Color GetColor(float normalTime, Color color);

		public override BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker)
		{
			EventDomain domain = GetSortedEventDomain(events);

			foreach (BaseEvent baseEvent in events)
			{
				float normalTime = GetNormalTime(domain, baseEvent);

				if (!(baseEvent.CustomColor is Color color))
					continue;

				BaseObject original = (BaseObject)baseEvent.Clone();
				baseEvent.CustomColor = GetColor(normalTime, color);
				baseEvent.WriteCustom();
				actionTracker.Modify(baseEvent, original);
			}

			return events;
		}
	}
}
