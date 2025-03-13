using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
using UnityEngine;
namespace Enlighten.Core
{
	public class FlutterEffect : Effect
	{
		private readonly BoolParameter m_populate = new BoolParameter(true, "Populate", "Whether to create alternating events.");
		private readonly FloatParameter m_multiplier = new FloatParameter(1, "Multiplier", "How much to multiply alternating events' brightness by.");
		private readonly FloatParameter m_turbulence = new FloatParameter(0, "Turbulence", "How much to randomly vary alternating event's brightness.");

		protected override IEnumerable<BaseParameter> GetParameters()
		{
			yield return m_populate;
			yield return m_multiplier;
			yield return m_turbulence;
		}

		public override BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker)
		{
			events = DoPopulation(events, actionTracker);
			return DoFluttering(events, actionTracker);
		}

		private BaseEvent[] DoPopulation(BaseEvent[] events, ActionTracker actionTracker)
		{
			List<BaseEvent> newEvents = new List<BaseEvent>(events);
			EventDomain domain = GetSortedEventDomain(events);

			for (int i = 0; i < events.Length; i++)
			{
				if (i == events.Length - 1)
					break;

				BaseEvent left = events[i];

				float normalTime = GetNormalTime(domain, left);
				bool populate = m_populate.Interpolate(normalTime);

				if (!populate)
					continue;

				BaseEvent right = events[i + 1];
				float midTime = (left.JsonTime + right.JsonTime) / 2;

				BaseEvent middle = (BaseEvent)left.Clone();
				middle.JsonTime = midTime;
				actionTracker.Add(middle);
				newEvents.Add(middle);
			}

			return newEvents.OrderBy(x => x.JsonTime).ToArray();
		}

		private BaseEvent[] DoFluttering(BaseEvent[] events, ActionTracker actionTracker)
		{
			EventDomain domain = GetSortedEventDomain(events);

			bool effected = true;

			foreach (BaseEvent baseEvent in events)
			{
				float normalTime = GetNormalTime(domain, baseEvent);

				if (!(baseEvent.CustomColor is Color color))
					continue;

				effected = !effected;

				if (!effected)
					continue;

				float multiplier = m_multiplier.Interpolate(normalTime);
				float turbulence = m_turbulence.Interpolate(normalTime);
				multiplier += Random.Range(-turbulence, turbulence);

				BaseObject original = (BaseObject)baseEvent.Clone();
				color.r *= multiplier;
				color.g *= multiplier;
				color.b *= multiplier;
				baseEvent.CustomColor = color;
				baseEvent.WriteCustom();
				actionTracker.Modify(baseEvent, original);
			}

			return events;
		}
	}
}
