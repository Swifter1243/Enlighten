using System;
using UnityEngine;
namespace Enlighten.UI
{
	public class ChartKeyframe<T> : MonoBehaviour
	{
		public event Action<Vector2> onMoved;
		public event Action onClicked;
	}
}
