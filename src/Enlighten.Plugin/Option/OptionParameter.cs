using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Enlighten.src.Enlighten.Plugin
{
	public abstract class OptionParameter<T> : MonoBehaviour
	{
		public UnityEvent<T> onValueChanged;

		private T _propertyValue;
		public T propertyValue
		{
			get => _propertyValue;
			set => onValueChanged.Invoke(_propertyValue = value);
		}

		public T defaultValue;
		public string propertyName;
		public string description;

		public abstract bool IsDefault();
		public void ToDefault()
		{
			propertyValue = defaultValue;
		}
		public void SetValueWithoutNotify(T value)
		{
			_propertyValue = value;
		}
	}
}
