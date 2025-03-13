using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;
namespace Enlighten.Core
{
	public class MainEffectsMode : EnlightenMode
	{
		public readonly static Dictionary<EffectName, Effect> s_effects = new Dictionary<EffectName, Effect>
		{
			[EffectName.Alpha] = new AlphaEffect("Alpha", "Adjusts the alpha channel of event's color."),
			[EffectName.Brightness] = new BrightnessEffect("Brightness", "Adjusts the brightness of events' color."),
			[EffectName.Flutter] = new FlutterEffect("Flutter", "Adds flickering events in between existing events or every other event."),
			[EffectName.Hue] = new HueEffect("Hue", "Adjusts the hue of event's color."),
			[EffectName.Saturation] = new SaturationEffect("Saturation", "Adjusts the saturation of event's color."),
			[EffectName.Pulse] = new PulseEffect("Pulse", "Periodically adjusts the brightness of events' color.")
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
