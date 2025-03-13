using System.Collections.Generic;
using Enlighten.Core;
using UnityEngine;
namespace Enlighten.UI
{
	internal class MainEffectsUI : MonoBehaviour
	{
		private MainEffectsMode m_mode;
		private BundleLoading.Assets m_assets;
		private readonly Dictionary<EffectName, OptionPanelUI> m_effectOptionPanels = new Dictionary<EffectName, OptionPanelUI>();
		private ParameterEditorHandler m_parameterEditorHandler;

		public void Initialize(MainEffectsMode mode, BundleLoading.Assets assets, ResizeableUI resizeableUI)
		{
			m_assets = assets;
			m_mode = mode;

			Transform content = transform.Find("Content");
			Transform leftContent = content.transform.Find("LeftContent");
			Transform rightContent = content.transform.Find("RightContent");

			m_parameterEditorHandler = gameObject.AddComponent<ParameterEditorHandler>();
			m_parameterEditorHandler.Initialize(assets, rightContent, resizeableUI);

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
