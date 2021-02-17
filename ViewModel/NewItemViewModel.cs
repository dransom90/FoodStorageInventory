using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class NewItemViewModel : INotifyPropertyChanged
	{
		public string FeedbackText { get; set; } = string.Empty;
		public string NewItemName { get; set; }
		public string NewItemQuantity { get; set; }

		public ICommand AddNewItemCommand => new DelegateCommand<object>(OnNewItemExecuted);

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnNewItemExecuted(object context)
		{
			if (FoodItemRepository.Instance.FoodItems.Any())
			{
				var results = FoodItemRepository.Instance.FoodItems.Where(x => x.Name == NewItemName);
				if (results.Any())
				{
					FeedbackText = "Item Already Exists!";
					PropertyChanged(this, new PropertyChangedEventArgs(nameof(FeedbackText)));
					return;
				}

				FeedbackText = "Item Added Successfully";
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(FeedbackText)));
			}

			FoodItemRepository.Instance.FoodItems.Add(new FoodItem() { Name = NewItemName, Quantity = int.Parse(NewItemQuantity) });
		}
	}
}