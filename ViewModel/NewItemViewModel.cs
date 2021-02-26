using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using log4net;
using Newtonsoft.Json;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class NewItemViewModel : INotifyPropertyChanged
	{
		private const string CONTAINER_FILE_PATH = @"C:\Program Files\Food Storage Inventory\containers.txt";
		private const string CONTAINER_DIRECTORY = @"C:\Program Files\Food Storage Inventory";
		private const string DEFAULT_CONTAINER = "Create New Container";

		private string _selectedContainer;
		private int _selectedIndex;
		private static readonly ILog _logger = LogManager.GetLogger(typeof(NewItemViewModel));

		public string FeedbackText { get; set; } = string.Empty;
		public string NewItemName { get; set; }
		public string NewItemQuantity { get; set; }
		public bool InputDialogVisible { get; set; }

		public int SelectedIndex
		{
			get => _selectedIndex;
			set
			{
				if (_selectedIndex == value)
				{
					return;
				}

				_selectedIndex = value;
				NotifyPropertyChanged(nameof(SelectedIndex));
			}
		}

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
					NotifyPropertyChanged(nameof(InputDialogVisible));
					return;
				}

				_selectedContainer = value;
				NotifyPropertyChanged(nameof(SelectedContainer));
			}
		}

		public ObservableCollection<string> AvailableContainers { get; set; }

		public ICommand AddNewItemCommand => new DelegateCommand<object>(OnNewItemExecuted);
		public ICommand AddNewContainerCommand => new DelegateCommand<object>(OnNewContainerAdded);
		public ICommand CancelNewContainerCommand => new DelegateCommand<object>(OnCancelNewContainer);
		public ICommand CancelNewItemCommand => new DelegateCommand<object>(OnCancelNewItem);

		public event PropertyChangedEventHandler PropertyChanged;

		public NewItemViewModel()
		{
			_ = ReadFromFile();
		}

		private void CloseWindow(Window window)
		{
			if (window != null)
			{
				window.Close();
			}
		}

		private void OnCancelNewItem(object context)
		{
			if (context is Window currentWindow)
			{
				CloseWindow(currentWindow);
			}
		}

		private void OnCancelNewContainer(object context)
		{
			InputDialogVisible = false;
			SelectedIndex = -1;
			NotifyPropertyChanged(nameof(InputDialogVisible));
			NotifyPropertyChanged(nameof(InputDialogVisible));
		}

		private void OnNewContainerAdded(object context)
		{
			if (context is string newContainer)
			{
				AvailableContainers.Add(newContainer);
				InputDialogVisible = false;
				SelectedContainer = newContainer;
				NotifyPropertyChanged(nameof(InputDialogVisible));
				NotifyPropertyChanged(nameof(AvailableContainers));
				NotifyPropertyChanged(nameof(SelectedContainer));
			}

			SaveToFile();
		}

		private void OnNewItemExecuted(object context)
		{
			if (LocationRepository.Instance.SelectedLocation is null && !LocationRepository.Instance.SelectedLocation.StoredFoodItems.Any())
			{
				UpdateFeedbackText("Please Fill Out All Fields");
				return;
			}

			var results = LocationRepository.Instance.SelectedLocation.StoredFoodItems.Where(x => x.Name == NewItemName);
			if (results.Any())
			{
				UpdateFeedbackText("Item Already Exists!");
				return;
			}

			UpdateFeedbackText("Item Added Successfully");

			FoodItem newFoodItem = new FoodItem(NewItemName, int.Parse(NewItemQuantity), SelectedContainer);
			LocationRepository.Instance.SelectedLocation.AddItem(newFoodItem);
			LocationRepository.Instance.SelectedFoodItem = newFoodItem;
		}

		private void UpdateFeedbackText(string text)
		{
			FeedbackText = text;
			NotifyPropertyChanged(nameof(FeedbackText));
		}

		public bool ReadFromFile()
		{
			try
			{
				using (StreamReader file = new StreamReader(CONTAINER_FILE_PATH))
				{
					var json = file.ReadToEnd();

					AvailableContainers = JsonConvert.DeserializeObject<ObservableCollection<string>>(json);
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error("Failed to read JSON file.  Creating empty collection.", ex);
				AvailableContainers = new ObservableCollection<string>() { DEFAULT_CONTAINER };
				return false;
			}
		}

		public bool SaveToFile()
		{
			try
			{
				if (!Directory.Exists(CONTAINER_DIRECTORY))
				{
					Directory.CreateDirectory(CONTAINER_DIRECTORY);
				}

				using (StreamWriter file = File.CreateText(CONTAINER_FILE_PATH))
				{
					JsonSerializer jsonSerializer = new JsonSerializer();
					var json = JsonConvert.SerializeObject(AvailableContainers);
					file.Write(json);
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error("Failed to write JSON file", ex);
				return false;
			}
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}