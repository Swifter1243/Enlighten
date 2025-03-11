using UnityEngine;
namespace Enlighten.Core
{
	public static class ChroMapperUtils
	{
		public static void Dialogue(string message)
		{
			PersistentUI.Instance.ShowDialogBox(message, null, PersistentUI.DialogBoxPresetType.Ok);
		}

		public static void AddTooltip(GameObject gameObject, string tooltip)
		{
			Tooltip component = gameObject.AddComponent<Tooltip>();
			component.TooltipOverride = tooltip;
		}
	}
}
