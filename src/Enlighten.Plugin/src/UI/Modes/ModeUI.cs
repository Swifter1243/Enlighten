using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	public class ModeUI : MonoBehaviour
	{
		public EnlightenMode m_enlightenMode;

		public void Initialize(EnlightenMode enlightenMode)
		{
			m_enlightenMode = enlightenMode;
			MakeUI(enlightenMode);
		}

		private void MakeUI(EnlightenMode enlightenMode)
		{
			switch (enlightenMode)
			{
			case MainEffectsMode mainEffectsMode:
				gameObject.AddComponent<MainEffectsUI>().Initialize(mainEffectsMode);
				break;
			case StripGeneratorMode stripGeneratorMode:
				gameObject.AddComponent<StripGeneratorUI>().Initialize(stripGeneratorMode);
				break;
			}
		}
	}
}
