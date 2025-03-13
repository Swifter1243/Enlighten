using System.Globalization;
using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
    public class RangeParameterUI : GenericParameterUI<float, RangeParameter>
    {
        private InputField m_inputField;
        private Slider m_slider;

        protected override void InitializeInternal(RangeParameter parameter)
        {
            m_slider = transform.Find("Slider").GetComponent<Slider>();
            m_slider.maxValue = parameter.m_maxValue;
            m_slider.minValue = parameter.m_minValue;
            m_slider.onValueChanged.AddListener(OnSliderValueChanged);
            m_inputField = transform.Find("InputField").GetComponent<InputField>();
            m_inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            SetCurrentValue(value);
            UpdateInputField();
        }

        private void OnInputFieldValueChanged(string value)
        {
            float newValue = float.Parse(value);
            SetCurrentValue(newValue);
            UpdateSlider();
        }

        public override void UpdateUI()
        {
            UpdateInputField();
            UpdateSlider();
        }

        private void UpdateInputField()
        {
            string text = CurrentValue.ToString(CultureInfo.InvariantCulture);
            m_inputField.SetTextWithoutNotify(text);
        }

        private void UpdateSlider()
        {
            m_slider.value = CurrentValue;
        }
    }
}
