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

		public void ClickHide()
		{
			var both = Input.GetKey(KeyCode.LeftShift);
			SetVisibility(false, true, both);
		}

		public void ClickDelete()
		{
			var both = Input.GetKey(KeyCode.LeftShift);
			panel.ToDefault(both);
			ClickHide();
		}

		public void ClickToggle()
		{
			var both = Input.GetKey(KeyCode.LeftShift);
			SetVisibility(!on, true, both);
		}

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

		public void SetVisibility(bool visible, bool write = true, bool bothSides = false)
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
				WriteEnable(on, bothSides);
			}
		}

		public void WriteEnable(bool enable, bool bothSides = false)
		{
			var start = bothSides ? enlightenPanel.startEnabledOptions : enlightenPanel.enabledOptions;
			var end = bothSides ? enlightenPanel.endEnabledOptions : enlightenPanel.enabledOptions;

			if (enable)
			{
				start.Add(optionName);
				if (bothSides) end.Add(optionName);
			}
			else
			{
				start.Remove(optionName);
				if (bothSides) end.Remove(optionName);
			}
		}

		public void LoadVisibility(HashSet<OptionName> enabledOptions)
		{
			var isPresent = enabledOptions.Contains(optionName);
			SetVisibility(isPresent, false);
		}
	}
}
