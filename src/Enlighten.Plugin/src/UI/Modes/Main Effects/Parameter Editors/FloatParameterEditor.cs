using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class FloatParameterEditor : ChartParameterEditor<float>
	{
		protected override float ChartPositionYToValue(float y) => y;

		protected override Vector2 HandleKeyframeMove(Vector2 chartPosition)
		{
			return chartPosition;
		}
	}
}
