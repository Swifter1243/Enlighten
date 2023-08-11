using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enlighten
{
	class UI
	{
		private Enlighten plugin;



		public UI(Enlighten plugin)
		{
			this.plugin = plugin;

			var button = new ExtensionButton();
			button.Tooltip = "Enlighten";
			ExtensionButtons.AddButton(button);
		}
	}
}
