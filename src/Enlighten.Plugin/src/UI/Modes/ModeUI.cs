﻿using UnityEngine;
namespace Enlighten.UI
{
	public abstract class ModeUI : MonoBehaviour
	{
		private Core.EnlightenMode m_enlightenMode;

		protected abstract void InitializeInternal();
		public void Initialize(Core.EnlightenMode enlightenMode)
		{
			m_enlightenMode = enlightenMode;
			InitializeInternal();
		}
	}
}
