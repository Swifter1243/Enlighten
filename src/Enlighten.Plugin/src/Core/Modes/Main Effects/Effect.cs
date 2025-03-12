using Beatmap.Base;
namespace Enlighten.Core
{
	public abstract class Effect
	{
		public bool m_isEnabled = false;

		public abstract BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker);
	}
}
