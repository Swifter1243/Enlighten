using System;
using System.Reflection;
using UnityEngine;

namespace Enlighten
{
	internal class BundleResource
	{
		private System.IO.Stream m_resourceStream;
		private AssetBundle m_bundle;
		private bool m_loaded = false;

		public AssetBundle Load()
		{
			if (!m_loaded)
			{
				m_resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Enlighten.enlighten_bundle");
				m_bundle = AssetBundle.LoadFromStream(m_resourceStream);
				m_loaded = true;
			}

			return m_bundle;
		}

		public void Dispose()
		{
			if (!m_loaded)
			{
				throw new Exception("The bundle isn't loaded, so it can't be disposed!");
			}

			m_bundle.UnloadAsync(false);
			m_resourceStream.Dispose();
			m_loaded = false;
		}
	}
}
