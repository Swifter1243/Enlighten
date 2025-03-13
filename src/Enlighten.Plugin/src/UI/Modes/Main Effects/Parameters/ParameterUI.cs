using System;
using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;
namespace Enlighten.UI
{
	public abstract class ParameterUI<T, TP> : MonoBehaviour where TP : GenericParameter<T>
	{
		private GenericParameter<T>.Keyframe m_selectedKeyframe;
		protected T CurrentValue => m_selectedKeyframe.m_value;
		public event Action onChange;

		protected abstract void InitializeInternal(TP parameter);

		public void Initialize(TP parameter)
		{
			Text nameText = transform.Find("Name").GetComponent<Text>();
			nameText.text = parameter.m_name;
			ChroMapperUtils.AddTooltip(nameText.gameObject, parameter.m_description);
			m_selectedKeyframe = parameter.m_keyframes[0];
			InitializeInternal(parameter);
			UpdateUI();
		}

		public void SetKeyframeInfo(T value, float time)
		{
			m_selectedKeyframe.m_time = time;
			m_selectedKeyframe.m_value = value;
			UpdateUI();
		}

		public void SetActiveKeyframe(GenericParameter<T>.Keyframe keyframe)
		{
			m_selectedKeyframe = keyframe;
			UpdateUI();
		}

		protected abstract void UpdateUI();

		protected void SetCurrentValue(T value)
		{
			m_selectedKeyframe.m_value = value;
			onChange?.Invoke();
		}
	}
}
