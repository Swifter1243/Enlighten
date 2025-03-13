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
            
            GameObject go = new GameObject("OptionPanel");
            
            foreach (BaseParameter effectParameter in effect.Parameters)
            {
                switch (effectParameter)
                {
                case FloatParameter floatParameter:
                        
                }
            }
        }
        
        
    }
}
