using System;
using Enlighten.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace Enlighten.UI
{
	public abstract class GenericParameterUI<T, TP> : BaseParameterUI where TP : GenericParameter<T>
	{
		public TP m_parameter;

		private GenericParameter<T>.Keyframe SelectedKeyframe
		{
			get => m_parameter[m_selectedKeyframeIndex];
			set => m_parameter[m_selectedKeyframeIndex] = value;
		}

		protected T CurrentValue
		{
			get => SelectedKeyframe.m_value;
			private set
			{
				GenericParameter<T>.Keyframe k = SelectedKeyframe;
				k.m_value = value;
				SelectedKeyframe = k;
			}
		}

		protected abstract void InitializeInternal(TP parameter);

		public void Initialize(TP parameter)
		{
			m_parameter = parameter;
			Text nameText = transform.Find("Name").GetComponent<Text>();
			nameText.text = m_parameter.m_name;
			ChroMapperUtils.AddTooltip(nameText.gameObject, m_parameter.m_description);
			InitializeInternal(parameter);
			UpdateUI();
		}

		public void SetKeyframeInfo(T value, float time)
		{
			GenericParameter<T>.Keyframe k = SelectedKeyframe;
			k.m_time = time;
			k.m_value = value;
			SelectedKeyframe = k;
			UpdateUI();
		}

		protected void SetCurrentValue(T value)
		{
			CurrentValue = value;
			m_onUIChanged.Invoke();
		}
	}
}
