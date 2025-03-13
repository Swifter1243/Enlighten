using System.Collections.Generic;
using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class MainEffectsUI : MonoBehaviour
	{
		private MainEffectsMode m_mode;
		private FloatValueEditorUI m_floatValueEditor;
		private BundleLoading.Assets m_assets;
		private readonly Dictionary<EffectName, OptionPanelUI> m_effectOptionPanels = new Dictionary<EffectName, OptionPanelUI>();
		private ParameterEditorHandler m_parameterEditorHandler;

		public void Initialize(MainEffectsMode mode, BundleLoading.Assets assets)
		{
			m_assets = assets;
			m_mode = mode;
			m_parameterEditorHandler = gameObject.AddComponent<ParameterEditorHandler>();

			GameObject content = transform.Find("Content").gameObject;
			GameObject leftContent = content.transform.Find("LeftContent").gameObject;
			GameObject rightContent = content.transform.Find("RightContent").gameObject;
			m_floatValueEditor = rightContent.transform.Find("ValueEditorWindow").gameObject.AddComponent<FloatValueEditorUI>();
			m_floatValueEditor.Initialize(m_assets);

			Transform effectOptionsParent = rightContent.transform.Find("EffectSettings").Find("Viewport").Find("Content");
			foreach (KeyValuePair<EffectName, Effect> kvp in MainEffectsMode.s_effects)
			{
				EffectName effectName = kvp.Key;
				Effect effect = kvp.Value;

				OptionPanelUI p = Instantiate(m_assets.m_mainEffectOptionPanel, effectOptionsParent).AddComponent<OptionPanelUI>();
				p.Initialize(m_assets, effect, m_parameterEditorHandler);
				m_effectOptionPanels[effectName] = p;
			}
		}
	}
}
