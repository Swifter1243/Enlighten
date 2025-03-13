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
                    RangeParameterUI rangeParameterUI = Instantiate(m_assets.m_rangeParameter, parametersParent).AddComponent<RangeParameterUI>();
                    rangeParameterUI.Initialize(rangeParameter);
                    rangeParameterUI.onUIValueChanged += parameterEditorHandler.SelectParameter;
                    break;
                case FloatParameter floatParameter:
                    FloatParameterUI floatParameterUI = Instantiate(m_assets.m_floatParameter, parametersParent).AddComponent<FloatParameterUI>();
                    floatParameterUI.Initialize(floatParameter);
                    floatParameterUI.onUIValueChanged += parameterEditorHandler.SelectParameter;
                    break;
                case BoolParameter boolParameter:
                    BoolParameterUI boolParameterUI = Instantiate(m_assets.m_boolParameter, parametersParent).AddComponent<BoolParameterUI>();
                    boolParameterUI.Initialize(boolParameter);
                    boolParameterUI.onUIValueChanged += parameterEditorHandler.SelectParameter;
                    break;
                }
            }
        }


    }
}
