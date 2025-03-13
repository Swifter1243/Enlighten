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
                switch (effectParameter)
                {
                case RangeParameter rangeParameter:
                    Instantiate(m_assets.m_rangeParameter, parametersParent);
                    break;
                case FloatParameter floatParameter:
                    Instantiate(m_assets.m_floatParameter, parametersParent);
                    break;
                case BoolParameter boolParameter:
                    Instantiate(m_assets.m_boolParameter, parametersParent);
                    break;
                }
            }
        }


    }
}
