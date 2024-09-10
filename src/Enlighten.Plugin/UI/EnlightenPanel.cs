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

			Draggable notchDraggable = insideContent.Find("Notch").gameObject.AddComponent<Draggable>();
			notchDraggable.Initialize(rectTransform, canvas);
		}
	}
}
