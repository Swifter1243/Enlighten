using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class LinkedDropdown<T> where T : Component
	{
		private T m_activeGameObject;
		private readonly List<T> m_children;
		public T ActiveObject => m_activeGameObject;

		public LinkedDropdown(Dropdown dropdown, List<T> children, T initialObject)
		{
			m_children = children;
			dropdown.ClearOptions();

			foreach (T child in children)
			{
				child.gameObject.SetActive(false);
				dropdown.options.Add(new Dropdown.OptionData(child.name));
			}

			dropdown.onValueChanged.AddListener(OnValueChanged);

			int initialObjectIndex = children.IndexOf(initialObject);
			if (initialObjectIndex == -1)
			{
				throw new Exception($"Couldn't find object '{initialObject.name}' in the provided list!");
			}
			dropdown.value = initialObjectIndex;
		}

		private void OnValueChanged(int newIndex) {
			m_activeGameObject.gameObject.SetActive(false);
			m_activeGameObject = m_children[newIndex];
			m_activeGameObject.gameObject.SetActive(true);
		}
	}
}
