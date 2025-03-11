using Beatmap.Base;
namespace Enlighten.Core
{
	public abstract class Effect
	{
		public abstract BaseEvent[] ProcessEvents(BaseEvent[] events, ActionTracker actionTracker);
	}
}
