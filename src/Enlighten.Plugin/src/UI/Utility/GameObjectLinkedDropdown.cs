using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class GameObjectLinkedDropdown
	{
		private GameObject m_activeGameObject;
		private readonly List<GameObject> m_children;

		public GameObjectLinkedDropdown(Dropdown dropdown, List<GameObject> children, GameObject initialObject)
		{
			m_children = children;
			dropdown.ClearOptions();

			foreach (GameObject child in children)
			{
				child.SetActive(false);
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
			m_activeGameObject.SetActive(false);
			m_activeGameObject = m_children[newIndex].gameObject;
			m_activeGameObject.SetActive(true);
		}
	}
}
