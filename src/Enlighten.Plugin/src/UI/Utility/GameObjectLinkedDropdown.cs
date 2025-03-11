using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.UI
{
	internal class GameObjectLinkedDropdown
	{
		GameObject activeGameObject;
		List<GameObject> children;

		public GameObjectLinkedDropdown(Dropdown dropdown, List<GameObject> children, string initialObjectName)
		{
			this.children = children;
			dropdown.ClearOptions();

			foreach (GameObject child in children)
			{
				child.SetActive(false);
				dropdown.options.Add(new Dropdown.OptionData(child.name));
			}

			dropdown.onValueChanged.AddListener(OnValueChanged);

			int initialObjectIndex = children.FindIndex(o => o.name == initialObjectName);
			if (initialObjectIndex == -1)
			{
				throw new Exception($"Couldn't find object '{initialObjectName}' in the provided list!");
			}
			dropdown.value = initialObjectIndex;
			activeGameObject = children[initialObjectIndex];
			activeGameObject.SetActive(true);
		}

		private void OnValueChanged(int newIndex) {
			activeGameObject.SetActive(false);
			activeGameObject = children[newIndex].gameObject;
			activeGameObject.SetActive(true);
		}
	}
}
