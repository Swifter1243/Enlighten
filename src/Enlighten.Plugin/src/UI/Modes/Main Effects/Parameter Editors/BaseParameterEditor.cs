using UnityEngine;
using UnityEngine.Events;
namespace Enlighten.UI
{
	internal abstract class BaseParameterEditor : MonoBehaviour
	{
		public readonly UnityEvent<int> m_onKeyframeChanged = new UnityEvent<int>();
		public readonly UnityEvent<int> m_onKeyframeSelected = new UnityEvent<int>();

		public abstract void RedrawCompletely();
	}
}
