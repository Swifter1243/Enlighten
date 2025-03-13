using Enlighten.Core;
using UnityEngine;

namespace Enlighten.UI
{
    internal class OptionPanelUI : MonoBehaviour
    {
        private BundleLoading.Assets m_assets;
        private Effect m_effect;

        public void Initialize(BundleLoading.Assets assets, Effect effect)
        {
            m_assets = assets;
            m_effect = effect;

            Transform parametersParent = transform.Find("Parameters");

            foreach (BaseParameter effectParameter in m_effect.Parameters)
            {
                ParameterUI parameterUI;
                switch (effectParameter)
                {
                case RangeParameter rangeParameter:
                    parameterUI = Instantiate(m_assets.m_rangeParameter, parametersParent).AddComponent<RangeParameterUI>();
                    parameterUI.Initialize(rangeParameter);
                    break;
                case FloatParameter floatParameter:
                    parameterUI = Instantiate(m_assets.m_floatParameter, parametersParent).AddComponent<FloatParameterUI>();
                    parameterUI.Initialize(floatParameter);
                    break;
                case BoolParameter boolParameter:
                    parameterUI = Instantiate(m_assets.m_boolParameter, parametersParent).AddComponent<BoolParameterUI>();
                    parameterUI.Initialize(boolParameter);
                    break;
                }
            }
        }


    }
}
