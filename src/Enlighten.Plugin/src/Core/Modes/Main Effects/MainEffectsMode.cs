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
			foreach (Effect effect in m_effectsWithOrder)
			{
				if (!effect.m_isEnabled)
					continue;

				events = effect.ProcessEvents(events, actionTracker);
			}

			return actionTracker.Finish();
		}
	}
}
