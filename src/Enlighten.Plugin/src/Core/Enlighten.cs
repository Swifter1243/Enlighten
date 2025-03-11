using System.Collections.Generic;
using Enlighten.Core.Strip_Generator;
namespace Enlighten.Core
{
	public class Enlighten
	{
		private EventGridContainer m_events;
		public Dictionary<Modes, EnlightenMode> m_modes;

		public Enlighten(EventGridContainer events)
		{
			m_events = events;
			m_modes = GetModes();
		}

		private static Dictionary<Modes, EnlightenMode> GetModes() => new Dictionary<Modes, EnlightenMode>
		{
			[Modes.MainEffects] = new MainEffectsMode(),
			[Modes.StripGenerator] = new StripGeneratorMode()
		};
	}
}
