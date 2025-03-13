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
