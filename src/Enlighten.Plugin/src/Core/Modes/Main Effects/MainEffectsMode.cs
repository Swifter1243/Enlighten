using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
namespace Enlighten.Core
{
	public class MainEffectsMode : EnlightenMode
	{
		public readonly static Dictionary<EffectName, Effect> s_effects = new Dictionary<EffectName, Effect>
		{
			[EffectName.Alpha] = new AlphaEffect(),
			[EffectName.Brightness] = new BrightnessEffect(),
			[EffectName.Flutter] = new FlutterEffect(),
			[EffectName.Hue] = new HueEffect(),
			[EffectName.Saturation] = new SaturationEffect(),
			[EffectName.Pulse] = new PulseEffect()
		};
		public readonly List<Effect> m_effectsWithOrder = s_effects.Values.ToList();

		public override IEnumerable<BeatmapAction> Execute(BaseEvent[] events, ActionTracker actionTracker)
		{
			m_effectsWithOrder.Aggregate(events, (current, effect) => effect.ProcessEvents(current, actionTracker));

			return actionTracker.Finish();
		}
	}
}
