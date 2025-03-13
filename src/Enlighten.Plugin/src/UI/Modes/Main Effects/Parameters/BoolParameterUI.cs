using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
    public class BoolParameterUI : ParameterUI<bool, BoolParameter>
    {
        private Toggle m_toggle;

        protected override void InitializeInternal(BoolParameter parameter)
        {
            m_toggle = transform.Find("Toggle").GetComponent<Toggle>();
            m_toggle.onValueChanged.AddListener(SetCurrentValue);
        }

        protected override void UpdateUI()
        {
            m_toggle.SetIsOnWithoutNotify(CurrentValue);
        }
    }
}
