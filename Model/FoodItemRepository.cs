using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using log4net;
using Newtonsoft.Json;

namespace Food_Storage_Inventory.Model
{
	public sealed class FoodItemRepository
	{
		private static readonly Lazy<FoodItemRepository> lazy = new Lazy<FoodItemRepository>(() => new FoodItemRepository());
		private static readonly ILog _logger = LogManager.GetLogger(typeof(FoodItemRepository));

		private const string INVENTORY_FILE_PATH = @"C:\Program Files\Food Storage Inventory\inventory.txt";
		private const string INVENTORY_DIRECTORY = @"C:\Program Files\Food Storage Inventory";

		public const string DEFAULT_NAME = "Create A New Item";

		public static FoodItemRepository Instance => lazy.Value;

		private FoodItemRepository()
		{
			_ = ReadFromFile();
			FoodItems.CollectionChanged += OnFoodItemsCollectionChanged;
		}

		private void OnFoodItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => SaveToFile();

		public ObservableCollection<FoodItem> FoodItems { get; set; }

		public void ResetAllQuantities() => FoodItems.ResetQuantities();

		public bool SaveToFile()
		{
			try
			{
				if (!Directory.Exists(INVENTORY_DIRECTORY))
				{
					Directory.CreateDirectory(INVENTORY_DIRECTORY);
				}

				using (StreamWriter file = File.CreateText(INVENTORY_FILE_PATH))
				{
					JsonSerializer jsonSerializer = new JsonSerializer();
					var json = JsonConvert.SerializeObject(FoodItems);
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

		public bool ReadFromFile()
		{
			try
			{
				using (StreamReader file = new StreamReader(INVENTORY_FILE_PATH))
				{
					var json = file.ReadToEnd();

					FoodItems = JsonConvert.DeserializeObject<ObservableCollection<FoodItem>>(json);
					CheckForDefaultEntry();
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error("Failed to read JSON file.  Creating empty collection.", ex);
				FoodItems = new ObservableCollection<FoodItem>()
				{
					new FoodItem(DEFAULT_NAME, 0, "NONE", false)
				};

				return false;
			}
		}

		public bool BackupFile()
		{
			return false;
		}

		private void CheckForDefaultEntry()
		{
			var names = FoodItems.Where(x => x.Name == DEFAULT_NAME);

			if (!names.Any())
			{
				FoodItems.Add(new FoodItem(DEFAULT_NAME, 0, "NONE", false));
			}
		}
	}
}