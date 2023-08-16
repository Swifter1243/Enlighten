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
		public string optionName;
		public OptionPanel panel;
		public Button button;
		public Image buttonImage;

		public void Toggle()
		{
			SetVisibility(!on);
		}

		public void SetVisibility(bool visible)
		{
			if (visible == on) return;

			on = visible;
			image.color = on ? Color.white : Color.gray;
			buttonImage.color = on ? Color.white : Color.black;
			panel.gameObject.SetActive(on);
		}

		public void LoadValues(Dictionary<string, float> vals)
		{
			var isPresent = vals.Keys.Any(x => x.Contains(optionName));
			SetVisibility(isPresent);
		}
	}
}
