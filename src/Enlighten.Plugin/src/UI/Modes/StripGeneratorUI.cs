using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	public class StripGeneratorUI : MonoBehaviour
	{
		private StripGeneratorMode m_mode;

		public void Initialize(StripGeneratorMode mode)
		{
			m_mode = mode;
		}
	}
}
