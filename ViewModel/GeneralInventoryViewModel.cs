using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Food_Storage_Inventory.Views;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class GeneralInventoryViewModel : INotifyPropertyChanged
	{
		private bool displayContinue = false;
		private bool errorTextVisible = false;
		private Location selectedLocation;
		private string quantityText;

		public FoodItem SelectedFoodItem { get; set; }

		public Location SelectedLocation
		{
			get => selectedLocation;
			set
			{
				if (selectedLocation == value)
					return;
				selectedLocation = value;
				LocationRepository.Instance.CurrentLocation = selectedLocation;
				ItemsInLocation = selectedLocation.StoredFoodItems;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemsInLocation)));
			}
		}

		public string UpdatedQuantity { get; set; } = "0";

		public ObservableCollection<FoodItem> ItemsInLocation { get; set; }

		public bool ErrorTextVisible
		{
			get => errorTextVisible;
			set
			{
				if (errorTextVisible == value)
					return;
				errorTextVisible = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorTextVisible)));
			}
		}

		public string QuantityText
		{
			get => quantityText;
			set
			{
				if (quantityText == value)
					return;
				quantityText = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuantityText)));
			}
		}

		public bool DisplayContinue
		{
			get => displayContinue;
			set
			{
				if (displayContinue == value)
					return;
				displayContinue = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayContinue)));
			}
		}

		public ICommand SaveCommand => new DelegateCommand<object>(OnSaveExecuted);

		public ICommand LocationSelectionChangedCommand => new DelegateCommand<object>(OnSelectedLocationChanged);
		public ICommand ItemSelectionChangedCommand => new DelegateCommand<object>(OnSelectedItemChanged);
		public ICommand ContinueCommand => new DelegateCommand<object>(OnCommandExecuted);

		public event PropertyChangedEventHandler PropertyChanged;

		public GeneralInventoryViewModel()
		{
			DisplayContinue = true;
			LocationRepository.Instance.SelectedFoodItem = null;
			LocationRepository.Instance.SelectedLocation = null;
		}

		private void OnCommandExecuted(object context)
		{
			DisplayContinue = false;
			LocationRepository.Instance.ResetAllQuantities();
		}

		private void OnSaveExecuted(object context)
		{
			if (LocationRepository.Instance.SelectedFoodItem is null || LocationRepository.Instance.SelectedLocation is null)
			{
				ErrorTextVisible = true;
				return;
			}

			bool success = LocationRepository.Instance.SelectedLocation.UpdateItem(LocationRepository.Instance.SelectedFoodItem, int.Parse(UpdatedQuantity));
			if (!success)
			{
				LocationRepository.Instance.SelectedFoodItem.Quantity = int.Parse(UpdatedQuantity);
				LocationRepository.Instance.SelectedLocation.AddItem(SelectedFoodItem);
			}

			SetQuantityText();
			ErrorTextVisible = false;
			LocationRepository.Instance.SaveToFile();
		}

		private void OnSelectedLocationChanged(object context)
		{
			if (!(LocationRepository.Instance.SelectedLocation is null))
			{
				RunNewLocation();
			}

			SetQuantityText();
		}

		private void OnSelectedItemChanged(object context)
		{
			if (!(LocationRepository.Instance.SelectedFoodItem is null))
			{
				RunNewItem();
			}

			SetQuantityText();
		}

		private void SetQuantityText()
		{
			if (LocationRepository.Instance.SelectedFoodItem is null)
			{
				QuantityText = string.Empty;
			}
			else
			{
				QuantityText = $"Current Quantity: {LocationRepository.Instance.SelectedFoodItem.Quantity}";
			}
		}

		private void RunNewItem()
		{
			if (LocationRepository.Instance.SelectedFoodItem.DisplayName.Contains(Location.DEFAULT_FOOD_ITEM))
			{
				NewItemPopup newItemPopup = new NewItemPopup();
				newItemPopup.Show();
				return;
			}
		}

		private void RunNewLocation()
		{
			if (LocationRepository.Instance.SelectedLocation.Name == LocationRepository.DEFAULT_ENTRY)
			{
				NewLocationWindow newLocationWindow = new NewLocationWindow();
				newLocationWindow.Show();
			}
		}
	}
}