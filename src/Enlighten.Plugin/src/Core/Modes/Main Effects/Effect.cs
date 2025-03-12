using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
using JetBrains.Annotations;
using UnityEngine;
namespace Enlighten.Core
{
	public abstract class Effect
	{
		public bool m_isEnabled = false;
		private List<BaseParameter> m_parameters;
		public List<BaseParameter> Parameters => m_parameters ?? (m_parameters = GetParameters().ToList());

		protected struct EventDomain
		{
			public float m_minTime;
			public float m_maxTime;
		}

		protected EventDomain GetSortedEventDomain(BaseEvent[] sortedEvents) => new EventDomain
		{
			m_minTime = sortedEvents[0].JsonTime,
			m_maxTime = sortedEvents[sortedEvents.Length - 1].JsonTime
		};
		protected float GetNormalTime(EventDomain eventDomain, BaseEvent baseEvent)
		{
			float beat = baseEvent.JsonTime;
			return Mathf.InverseLerp(eventDomain.m_minTime, eventDomain.m_maxTime, beat);
		}

		protected abstract IEnumerable<BaseParameter> GetParameters();
		public abstract BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker);
	}
}
