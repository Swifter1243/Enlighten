using System.Collections.Generic;
using Beatmap.Base;
using UnityEngine;
namespace Enlighten.Core
{
	public class PulseEffect : Effect
	{
		private readonly FloatParameter m_intensity = new FloatParameter(1, "Intensity", "The contribution of pulsing to apply to events.");
		private readonly FloatParameter m_frequency = new FloatParameter(1, "Frequency", "The speed of pulsing to apply to events.");

		protected override IEnumerable<BaseParameter> GetParameters()
		{
			yield return m_intensity;
			yield return m_frequency;
		}

		public override BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker)
		{
			EventDomain domain = GetSortedEventDomain(events);

			float lastBeat = domain.m_minTime;
			float wavePosition = 0;

			foreach (BaseEvent baseEvent in events)
			{
				float normalTime = GetNormalTime(domain, baseEvent);

				float beat = baseEvent.JsonTime;
				float deltaTime = beat - lastBeat;
				lastBeat = beat;

				float frequency = m_frequency.Interpolate(normalTime);
				wavePosition += deltaTime * frequency;
				float intensity = m_intensity.Interpolate(normalTime);
				float multiplier = 1 + Mathf.Sin(wavePosition) * intensity;

				if (!(baseEvent.CustomColor is Color color))
					continue;

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
