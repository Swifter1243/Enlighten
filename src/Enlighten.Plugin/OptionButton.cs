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
			panel.ToDefault();
			SetVisibility(false);
		}

		public void Toggle()
		{
			SetVisibility(!on);
		}

		public static Color DARK_GREY = new Color(0.2f, 0.2f, 0.2f);
		public static Color GRAY_BLUE = new Color(0.71f, 0.87f, 0.97f);

		public void SetVisibility(bool visible, bool write = true)
		{
			on = visible;
			image.color = on ? Color.white : Color.gray;
			buttonImage.color = on ? Color.white : DARK_GREY;

			if (!on && !panel.IsDefault())
			{
				buttonImage.color = GRAY_BLUE;
			}

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
