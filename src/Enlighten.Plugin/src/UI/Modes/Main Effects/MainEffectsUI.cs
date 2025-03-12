using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class MainEffectsUI : MonoBehaviour
	{
		private MainEffectsMode m_mode;
		private FloatValueEditorUI m_floatValueEditor;
		private BundleLoading.Assets m_assets;

		public void Initialize(MainEffectsMode mode, BundleLoading.Assets assets)
		{
			m_mode = mode;
			
			GameObject content = transform.Find("Content").gameObject;
			GameObject leftContent = content.transform.Find("LeftContent").gameObject;
			GameObject rightContent = content.transform.Find("RightContent").gameObject;
			FloatValueEditorUI floatValueEditorUI = rightContent.transform.Find("ValueEditorWindow").gameObject.AddComponent<FloatValueEditorUI>();
			
			floatValueEditorUI.Initialize(m_assets);
		}
	}
}
