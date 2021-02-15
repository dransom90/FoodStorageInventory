using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class GeneralInventoryViewModel : INotifyPropertyChanged
	{
		public GeneralInventoryViewModel()
		{
			//FoodItems = FoodItemRepository.Instance.FoodItems;
		}

		//public List<ObservableCollection> FoodItems { get; set; }

		public string NewItemName { get; set; }
		public string NewItemQuantity { get; set; }
		public string NewItemContainerDescription { get; set; }
		public string SelectedOption { get; set; }

		public ICommand AddNewItemCommand => new DelegateCommand<object>(OnNewItemExecuted);
		public ICommand NewItemButtonCommand => new DelegateCommand<object>(OnNewItemButtonExecuted);

		public ICommand UpdateItemCommand => new DelegateCommand<object>(OnUpdateItemExecuted);

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnNewItemExecuted(object context)
		{
			FoodItemRepository.Instance.FoodItems.Add(new FoodItem() { Name = NewItemName, Quantity = int.Parse(NewItemQuantity), ContainerDescription = NewItemContainerDescription });

			//PropertyChanged(this, new PropertyChangedEventArgs(nameof(FoodItems)));
		}

		private void OnNewItemButtonExecuted(object context)
		{
			NewItemPopup newItemPopup = new NewItemPopup();
			newItemPopup.Show();
		}

		private void OnUpdateItemExecuted(object context)
		{
		}
	}
}