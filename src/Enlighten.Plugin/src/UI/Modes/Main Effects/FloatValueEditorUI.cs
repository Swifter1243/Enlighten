using Enlighten.Core;
using UnityEngine;

namespace Enlighten.UI
{
    internal class FloatValueEditorUI : MonoBehaviour
    {
        private GameObject m_pointPrefab;
        private FloatParameter m_floatParameter;
        
        public void Initialize(ref BundleLoading.Assets assets)
        {
            m_pointPrefab = assets.m_pointPrefab;
        }

        public void SetActiveParameter(FloatParameter parameter)
        {
            m_floatParameter = parameter;
        }
    }
}
