using UnityEngine;

namespace Enlighten.UI
{
	internal class MappingSceneUIFactory
	{
		public readonly EnlightenPanelFactory m_enlightenPanelFactory;

		public MappingSceneUIFactory(ref BundleLoading.Assets assets)
		{
			m_enlightenPanelFactory = new EnlightenPanelFactory(ref assets);
		}
	}
}
