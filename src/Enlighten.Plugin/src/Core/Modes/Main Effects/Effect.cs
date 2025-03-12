using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
using JetBrains.Annotations;
namespace Enlighten.Core
{
	public abstract class Effect
	{
		public bool m_isEnabled = false;
		private List<BaseParameter> m_parameters;
		public List<BaseParameter> Parameters => m_parameters ?? (m_parameters = GetParameters().ToList());

		protected abstract IEnumerable<BaseParameter> GetParameters();
		public abstract BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker);
	}
}
