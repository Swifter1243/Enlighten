using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	public class MainEffectsUI : MonoBehaviour
	{
		private MainEffectsMode m_mode;

		public void Initialize(MainEffectsMode mode)
		{
			m_mode = mode;
		}
	}
}
