using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class NewItemViewModel : INotifyPropertyChanged
	{
		private string _selectedContainer;

		public string FeedbackText { get; set; } = string.Empty;
		public string NewItemName { get; set; }
		public string NewItemQuantity { get; set; }
		public bool InputDialogVisible { get; set; }

		public string SelectedContainer
		{
			get => _selectedContainer;
			set
			{
				if (_selectedContainer == value)
				{
					return;
				}

				if (value == "Create New Container")
				{
					InputDialogVisible = true;
					PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputDialogVisible)));
					return;
				}

				_selectedContainer = value;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedContainer)));
			}
		}

		public ObservableCollection<string> AvailableContainers { get; set; }

		public ICommand AddNewItemCommand => new DelegateCommand<object>(OnNewItemExecuted);
		public ICommand AddNewContainerCommand => new DelegateCommand<object>(OnNewContainerAdded);
		public ICommand CancelNewContainerCommand => new DelegateCommand<object>(OnCancelNewContainer);

		public event PropertyChangedEventHandler PropertyChanged;

		public NewItemViewModel()
		{
			AvailableContainers = new ObservableCollection<string>() { "Create New Container" };    // TODO:	Read From File
		}

		private void OnCancelNewContainer(object context)
		{
			InputDialogVisible = false;
			PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputDialogVisible)));
		}

		private void OnNewContainerAdded(object context)
		{
			if (context is string newContainer)
			{
				AvailableContainers.Add(newContainer);
				InputDialogVisible = false;
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(InputDialogVisible)));
				PropertyChanged(this, new PropertyChangedEventArgs(nameof(AvailableContainers)));
			}

			// TODO:	Save To File
		}

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

			FoodItemRepository.Instance.FoodItems.Add(new FoodItem(NewItemName, int.Parse(NewItemQuantity), SelectedContainer));
		}
	}
}