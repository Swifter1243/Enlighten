using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class MappingSceneUIFactory
	{
		public EnlightenPanelFactory enlightenPanelFactory;

		public MappingSceneUIFactory(AssetBundle bundle)
		{
			enlightenPanelFactory = new EnlightenPanelFactory(bundle);
		}
	}
}
