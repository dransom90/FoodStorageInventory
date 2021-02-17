using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class GeneralInventoryViewModel : INotifyPropertyChanged
	{
		public string NewItemName { get; set; }
		public string NewItemQuantity { get; set; }
		public string NewItemContainerDescription { get; set; }
		public string SelectedOption { get; set; }
		public string UpdatedQuantity { get; set; } = "0";
		public string ErrorText { get; set; } = string.Empty;
		public string SelectedItemQuantity { get; set; } = string.Empty;

		public ICommand AddNewItemCommand => new DelegateCommand<object>(OnNewItemExecuted);
		public ICommand NewItemButtonCommand => new DelegateCommand<object>(OnNewItemButtonExecuted);

		public ICommand UpdateItemCommand => new DelegateCommand<object>(OnUpdateItemExecuted);
		public ICommand SelectedItemChangedCommand => new DelegateCommand<object>(OnSelectedItemChanged);
		public ICommand SaveCommand => new DelegateCommand<object>(OnSaveExecuted);
		public ICommand BackUpCommand => new DelegateCommand<object>(OnBackupExecuted);

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnSaveExecuted(object context) => FoodItemRepository.Instance.SaveToFile();

		private void OnBackupExecuted(object context) => FoodItemRepository.Instance.BackupFile();

		private void OnNewItemExecuted(object context)
		{
			if (FoodItemRepository.Instance.FoodItems.Any())
			{
				var results = FoodItemRepository.Instance.FoodItems.Where(x => x.Name == NewItemName);
				if (results.Any())
				{
					ErrorText = "Item Already Exists!";
					PropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorText)));
					return;
				}

				ErrorText = string.Empty;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorText)));
			}

			FoodItemRepository.Instance.FoodItems.Add(new FoodItem() { Name = NewItemName, Quantity = int.Parse(NewItemQuantity) });
		}

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