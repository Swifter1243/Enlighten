using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten
{
	internal class BundleResource
	{
		private System.IO.Stream resourceStream;
		private AssetBundle bundle;
		bool loaded = false;

		public AssetBundle Load()
		{
			if (!loaded)
			{
				resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Enlighten.enlighten_bundle");
				bundle = AssetBundle.LoadFromStream(resourceStream);
				loaded = true;
			}

			return bundle;
		}

		public void Dispose()
		{
			if (!loaded)
			{
				throw new Exception("The bundle isn't loaded, so it can't be disposed!");
			}

			bundle.UnloadAsync(false);
			resourceStream.Dispose();
			loaded = false;
		}
	}
}
