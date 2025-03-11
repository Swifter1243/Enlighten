using System.Collections.Generic;
using Beatmap.Base;
namespace Enlighten.Core
{
	public abstract class EnlightenMode
	{
		public abstract IEnumerable<BeatmapAction> Execute(BaseEvent[] events, ActionTracker actionTracker);
	}
}
