﻿using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;

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

            Transform top = transform.Find("Top");
            Text titleText = top.Find("Title").GetComponent<Text>();
            titleText.text = effect.m_name;
            ChroMapperUtils.AddTooltip(titleText.gameObject, effect.m_description);

            InitializeParameters();
        }

        private void InitializeParameters()
        {
            Transform parametersParent = transform.Find("Parameters");

            foreach (BaseParameter effectParameter in m_effect.Parameters)
            {
                switch (effectParameter)
                {
                case RangeParameter rangeParameter:
                    RangeParameterUI rangeParameterUI = Instantiate(m_assets.m_rangeParameter, parametersParent).AddComponent<RangeParameterUI>();
                    rangeParameterUI.Initialize(rangeParameter);
                    break;
                case FloatParameter floatParameter:
                    FloatParameterUI floatParameterUI = Instantiate(m_assets.m_floatParameter, parametersParent).AddComponent<FloatParameterUI>();
                    floatParameterUI.Initialize(floatParameter);
                    break;
                case BoolParameter boolParameter:
                    BoolParameterUI boolParameterUI = Instantiate(m_assets.m_boolParameter, parametersParent).AddComponent<BoolParameterUI>();
                    boolParameterUI.Initialize(boolParameter);
                    break;
                }
            }
        }


    }
}
