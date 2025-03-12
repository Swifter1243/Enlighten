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
		}
	}
}
