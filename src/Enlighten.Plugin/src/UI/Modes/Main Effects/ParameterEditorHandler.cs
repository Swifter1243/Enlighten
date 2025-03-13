using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class ParameterEditorHandler : MonoBehaviour
	{
		private FloatParameterEditor m_floatParameterEditor;

		public void Initialize(BundleLoading.Assets assets, Transform rightContent)
		{
			m_floatParameterEditor = rightContent.Find("FloatParameterEditor").gameObject.AddComponent<FloatParameterEditor>();
			m_floatParameterEditor.Initialize(assets);
		}

		public void SelectFloatParameter(FloatParameterUI floatParameterUI)
		{
			m_floatParameterEditor.OpenParameter(floatParameterUI.m_parameter);
			floatParameterUI.onUIValueChanged += m_floatParameterEditor.RedrawCompletely;
			m_floatParameterEditor.onKeyframeMoved += _ => floatParameterUI.UpdateUI();
			m_floatParameterEditor.onKeyframeSelected += floatParameterUI.SetActiveKeyframeIndex;
		}

		public void SelectRangeParameter(RangeParameterUI rangeParameterUI)
		{
			// TODO
		}

		public void SelectBoolParameter(BoolParameterUI boolParameterUI)
		{
			// TODO
		}
	}
}
