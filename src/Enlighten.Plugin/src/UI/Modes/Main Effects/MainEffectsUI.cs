using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	public class MainEffectsUI : MonoBehaviour
	{
		private MainEffectsMode m_mode;
		private FloatValueEditorUI m_floatValueEditor;

		public void Initialize(MainEffectsMode mode)
		{
			m_mode = mode;
			
			GameObject content = transform.Find("Content").gameObject;
			GameObject leftContent = content.transform.Find("LeftContent").gameObject;
			GameObject rightContent = content.transform.Find("RightContent").gameObject;
			FloatValueEditorUI floatValueEditorUI = rightContent.transform.Find("ValueEditorWindow").gameObject.AddComponent<FloatValueEditorUI>();
			
			
		}
	}
}
