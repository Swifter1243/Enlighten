using Enlighten.Core;
using UnityEngine;
using UnityEngine.UI;
namespace Enlighten.UI
{
	public class ParameterUI : MonoBehaviour
	{
		public void Initialize(BaseParameter parameter)
		{
			Text nameText = transform.Find("Name").GetComponent<Text>();
			nameText.text = parameter.m_name;
			ChroMapperUtils.AddTooltip(nameText.gameObject, parameter.m_description);
		}
	}
}
