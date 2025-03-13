using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
    internal class OptionPanelUI : MonoBehaviour
    {
        private BundleLoading.Assets m_assets;
        private Effect m_effect;

        public void Initialize(BundleLoading.Assets assets, Effect effect, ParameterEditorHandler parameterEditorHandler)
        {
            m_assets = assets;
            m_effect = effect;

            Transform top = transform.Find("Top");
            Text titleText = top.Find("Title").GetComponent<Text>();
            titleText.text = effect.m_name;
            ChroMapperUtils.AddTooltip(titleText.gameObject, effect.m_description);

            InitializeParameters(parameterEditorHandler);
        }

        private void InitializeParameters(ParameterEditorHandler parameterEditorHandler)
        {
            Transform parametersParent = transform.Find("Parameters");

            foreach (BaseParameter effectParameter in m_effect.Parameters)
            {
                switch (effectParameter)
                {
                case RangeParameter rangeParameter:
                    SetupRangeParameter(parameterEditorHandler, parametersParent, rangeParameter);
                    break;
                case FloatParameter floatParameter:
                    SetupFloatParameter(parameterEditorHandler, parametersParent, floatParameter);
                    break;
                case BoolParameter boolParameter:
                    SetupBoolParameter(parameterEditorHandler, parametersParent, boolParameter);
                    break;
                }
            }
        }

        private void SetupRangeParameter(ParameterEditorHandler parameterEditorHandler, Transform parametersParent, RangeParameter rangeParameter)
        {
            RangeParameterUI parameterUI = Instantiate(m_assets.m_rangeParameter, parametersParent).AddComponent<RangeParameterUI>();
            parameterUI.Initialize(rangeParameter);
            parameterUI.m_onInteracted.AddListener(() => parameterEditorHandler.SelectRangeParameter(parameterUI));
        }
        private void SetupFloatParameter(ParameterEditorHandler parameterEditorHandler, Transform parametersParent, FloatParameter floatParameter)
        {
            FloatParameterUI parameterUI = Instantiate(m_assets.m_floatParameter, parametersParent).AddComponent<FloatParameterUI>();
            parameterUI.Initialize(floatParameter);
            parameterUI.m_onInteracted.AddListener(() => parameterEditorHandler.SelectFloatParameter(parameterUI));
        }
        private void SetupBoolParameter(ParameterEditorHandler parameterEditorHandler, Transform parametersParent, BoolParameter boolParameter)
        {
            BoolParameterUI parameterUI = Instantiate(m_assets.m_boolParameter, parametersParent).AddComponent<BoolParameterUI>();
            parameterUI.Initialize(boolParameter);
            parameterUI.m_onInteracted.AddListener(() => parameterEditorHandler.SelectBoolParameter(parameterUI));
        }
    }
}
