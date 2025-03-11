using UnityEngine;

namespace Enlighten.UI
{
	internal class MappingSceneUIFactory
	{
		public readonly EnlightenPanelFactory m_enlightenPanelFactory;

		public MappingSceneUIFactory(AssetBundle bundle)
		{
			m_enlightenPanelFactory = new EnlightenPanelFactory(bundle);
		}
	}
}
