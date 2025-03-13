using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class ParameterEditorHandler : MonoBehaviour
	{
		private BaseParameterUI m_currentParameterUI;
		private BaseParameterEditor m_currentParameterEditor;

		private FloatParameterEditor m_floatParameterEditor;

		public void Initialize(BundleLoading.Assets assets, Transform rightContent)
		{
			m_floatParameterEditor = rightContent.Find("FloatParameterEditor").gameObject.AddComponent<FloatParameterEditor>();
			m_floatParameterEditor.Initialize(assets);
		}

		public void SelectFloatParameter(FloatParameterUI floatParameterUI)
		{
			if (floatParameterUI == m_currentParameterUI)
				return;

			m_currentParameterUI = floatParameterUI;
			m_currentParameterEditor = m_floatParameterEditor;
			m_floatParameterEditor.OpenParameter(floatParameterUI.m_parameter);
			AddListeners();
		}

		public void SelectRangeParameter(RangeParameterUI rangeParameterUI)
		{
			// TODO
		}

		public void SelectBoolParameter(BoolParameterUI boolParameterUI)
		{
			// TODO
		}

		private void AddListeners()
		{
			m_currentParameterUI.m_onUIChanged.AddListener(RedrawCurrentEditor);
			m_currentParameterEditor.m_onKeyframeChanged.AddListener(UpdateCurrentParameterUI);
			m_currentParameterEditor.m_onKeyframeSelected.AddListener(SetCurrentParameterUIKeyframeIndex);
		}
		private void UpdateCurrentParameterUI(int _)
		{
			m_currentParameterUI.UpdateUI();
		}
		private void SetCurrentParameterUIKeyframeIndex(int index)
		{
			m_currentParameterUI.SetActiveKeyframeIndex(index);
		}
		private void RedrawCurrentEditor()
		{
			m_currentParameterEditor.RedrawCompletely();
		}
	}
}
