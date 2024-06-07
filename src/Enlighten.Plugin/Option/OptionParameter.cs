using Enlighten.src.Enlighten.Plugin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Enlighten.src.Enlighten.Plugin
{
	public abstract class TypedOptionParameter<T> : OptionParameter
	{
		public UnityEvent<T> onValueChanged;

		private T _propertyValue;
		public T propertyValue
		{
			get => _propertyValue;
			set => onValueChanged.Invoke(_propertyValue = value);
		}

		public T defaultValue;

		public override void ToDefault()
		{
			propertyValue = defaultValue;
		}
		public void SetValueWithoutNotify(T value)
		{
			_propertyValue = value;
		}
	}


	public abstract class OptionParameter : MonoBehaviour
	{
		public abstract bool IsDefault();
		public abstract void ToDefault();

		public abstract void ReadData(OptionParameterData data);
		public abstract void WriteData(OptionParameterData data);
	}
}
