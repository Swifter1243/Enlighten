using System.Collections.Generic;
using Beatmap.Base;
namespace Enlighten.Core.Strip_Generator
{
	public class StripGeneratorMode : EnlightenMode
	{
		public override IEnumerable<BeatmapAction> Execute(BaseEvent[] events, ActionTracker actionTracker)
		{
			return actionTracker.Finish();
		}
	}
}
