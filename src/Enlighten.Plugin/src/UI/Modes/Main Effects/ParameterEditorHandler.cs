using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;
namespace Enlighten.UI
{
	internal class ParameterEditorHandler : MonoBehaviour
	{
		private BaseParameterUI m_currentParameterUI;
		private BaseParameterEditor m_currentParameterEditor;

		private FloatParameterEditor m_floatParameterEditor;

		private RectTransform m_editorParent;

		public void Initialize(BundleLoading.Assets assets, Transform rightContent, ResizeableUI resizeableUI)
		{
			m_floatParameterEditor = rightContent.Find("FloatParameterEditor").gameObject.AddComponent<FloatParameterEditor>();
			m_floatParameterEditor.Initialize(assets);
			m_floatParameterEditor.gameObject.SetActive(false);
			resizeableUI.m_onResize.AddListener(RedrawCurrentEditor);
			m_editorParent = rightContent.GetComponent<RectTransform>();
		}

		public void SelectFloatParameter(FloatParameterUI floatParameterUI)
		{
			if (floatParameterUI == m_currentParameterUI)
				return;

			m_floatParameterEditor.OpenParameter(floatParameterUI.m_parameter);
			SetupParameter(floatParameterUI, m_floatParameterEditor);
		}

		public void SelectRangeParameter(RangeParameterUI rangeParameterUI)
		{
			// TODO
		}

		public void SelectBoolParameter(BoolParameterUI boolParameterUI)
		{
			// TODO
		}

		private void SetupParameter(BaseParameterUI parameterUI, BaseParameterEditor parameterEditor)
		{
			if (m_currentParameterEditor != null)
			{
				m_currentParameterEditor.gameObject.SetActive(false);
				m_currentParameterEditor = null;
			}

			m_currentParameterUI = parameterUI;
			m_currentParameterEditor = parameterEditor;
			m_currentParameterEditor.gameObject.SetActive(true);
			LayoutRebuilder.ForceRebuildLayoutImmediate(m_editorParent);
			m_currentParameterEditor.RedrawCompletely();

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
			if (m_currentParameterEditor == null)
				return;

			m_currentParameterEditor.RedrawCompletely();
		}
	}
}
