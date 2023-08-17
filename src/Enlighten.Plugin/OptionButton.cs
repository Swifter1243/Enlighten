using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Enlighten.src.Enlighten.Plugin
{
	public class OptionButton : MonoBehaviour
	{
		public bool on = true;
		public RawImage image;
		public OptionName optionName;
		public OptionPanel panel;
		public Button button;
		public Image buttonImage;
		public EnlightenPanel enlightenPanel;

		public void Clear()
		{
			SetVisibility(false);
			panel.ToDefault();
		}

		public void Toggle()
		{
			SetVisibility(!on);
		}

		public void SetVisibility(bool visible, bool write = true)
		{
			if (visible == on) return;

			on = visible;
			image.color = on ? Color.white : Color.gray;
			buttonImage.color = on ? Color.white : Color.black;
			panel.gameObject.SetActive(on);

			if (write)
			{
				if (on)
				{
					enlightenPanel.enabledOptions.Add(optionName);
				}
				else
				{
					enlightenPanel.enabledOptions.Remove(optionName);
				}
			}
		}

		public void LoadVisibility(HashSet<OptionName> enabledOptions)
		{
			var isPresent = enabledOptions.Contains(optionName);
			SetVisibility(isPresent, false);
		}
	}
}
