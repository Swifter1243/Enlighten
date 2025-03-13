using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class FloatParameterEditor : ChartParameterEditor<float>
	{
		protected override float ChartYPositionToValue(float y) => y;
		protected override float ValueToChartYPosition(float value) => value;
	}
}
