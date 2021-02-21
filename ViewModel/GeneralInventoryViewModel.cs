using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class GeneralInventoryViewModel : INotifyPropertyChanged
	{
		public string SelectedOption { get; set; }
		public string UpdatedQuantity { get; set; } = "0";

		public string SelectedItemQuantity { get; set; } = string.Empty;

		public ICommand NewItemButtonCommand => new DelegateCommand<object>(OnNewItemButtonExecuted);

		public ICommand UpdateItemCommand => new DelegateCommand<object>(OnUpdateItemExecuted);
		public ICommand SelectedItemChangedCommand => new DelegateCommand<object>(OnSelectedItemChanged);

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnSelectedItemChanged(object context)
		{
			var results = FoodItemRepository.Instance.FoodItems.Where(x => x.Name == SelectedOption);

			if (!results.Any())
				return;

			SelectedItemQuantity = $"Quantity: {results.First().Quantity}";
			PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedItemQuantity)));
		}

		private void OnNewItemButtonExecuted(object context)
		{
			NewItemPopup newItemPopup = new NewItemPopup();
			newItemPopup.Show();
		}

		private void OnUpdateItemExecuted(object context)
		{
			var results = FoodItemRepository.Instance.FoodItems.Where(x => x.Name == SelectedOption);

			if (!results.Any())
				return;

			FoodItem foodItem = results.First();
			foodItem.Quantity = int.Parse(UpdatedQuantity);

			SelectedItemQuantity = $"Quantity: {foodItem.Quantity}";
			PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedItemQuantity)));
		}
	}
}