using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using System.Diagnostics;
using System.Windows.Navigation;

namespace Food_Storage_Inventory.ViewModel
{
	public class AboutViewModel
	{
		public ICommand NavigateCommand => new DelegateCommand<object>(OnNavigate);

		private void OnNavigate(object context)
		{
		}
	}
}