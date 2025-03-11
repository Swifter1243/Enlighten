using System.Collections.Generic;
namespace Enlighten.Core
{
	public abstract class EnlightenMode
	{
		public abstract IEnumerable<BeatmapAction> Execute(EventGridContainer events);
	}
}
