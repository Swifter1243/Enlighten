using System.Globalization;
using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
    public class FloatParameterUI : GenericParameterUI<float, FloatParameter>
    {
        private InputField m_inputField;

        protected override void InitializeInternal(FloatParameter parameter)
        {
            m_inputField = transform.Find("InputField").gameObject.GetComponent<InputField>();
            m_inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        public override void UpdateUI()
        {
            string text = CurrentValue.ToString(CultureInfo.InvariantCulture);
            m_inputField.SetTextWithoutNotify(text);
        }

        private void OnInputFieldValueChanged(string value)
        {
            float newValue = float.Parse(value);
            SetCurrentValue(newValue);
        }
    }
}
