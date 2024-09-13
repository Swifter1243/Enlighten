using Enlighten.src.Enlighten.Plugin.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Enlighten.src.Enlighten.Plugin.UI
{
	internal class EnlightenPanel : MonoBehaviour, IFunctionalUI
	{
		public void Hookup(RectTransform canvas)
		{
			RectTransform rectTransform = GetComponent<RectTransform>();
			Transform insideContent = transform.Find("InsideContent");

			GameObject notch = insideContent.Find("Notch").gameObject;
			DraggableUI notchDraggable = notch.AddComponent<DraggableUI>();
			notchDraggable.Initialize(rectTransform, canvas);


		}
	}
}
