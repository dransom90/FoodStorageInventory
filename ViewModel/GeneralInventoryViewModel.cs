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
		public FoodItem SelectedFoodItem { get; set; }
		public Location SelectedLocation { get; set; }

		public string UpdatedQuantity { get; set; } = "0";

		public ICommand SaveCommand => new DelegateCommand<object>(OnSaveExecuted);

		public event PropertyChangedEventHandler PropertyChanged;

		public GeneralInventoryViewModel()
		{
			// TODO:	Reset all quantities to zero
		}

		private void OnSaveExecuted(object context)
		{
			var temp = FoodItemRepository.Instance.FoodItems;
		}

		private void UpdateItemInventory()
		{
			var newQuantity = int.Parse(UpdatedQuantity);
			// Assign UpdatedQuanity of SelectedFoodItem Name to SelectedLocation
			for (int i = 0; i < newQuantity; i++)
			{
				SelectedLocation.StoredFoodItems.Add(SelectedFoodItem.Name);
			}

			// Increment SelectedFoodItem's Quantity
			SelectedFoodItem.Quantity += newQuantity;
		}
	}
}