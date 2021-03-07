using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class EditItemsViewModel : INotifyPropertyChanged
	{
		public ICommand UpdateCommand => new DelegateCommand<object>(OnUpdateCommand);

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnUpdateCommand(object context)
		{ }

		public FoodItem SelectedItem { get; set; }

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}