using System;
using System.Collections.ObjectModel;
using System.IO;

//using Json.Net;
using Newtonsoft.Json;
using log4net;

namespace Food_Storage_Inventory.Model
{
	public sealed class FoodItemRepository
	{
		private static readonly Lazy<FoodItemRepository> lazy = new Lazy<FoodItemRepository>(() => new FoodItemRepository());
		private static readonly ILog _logger = LogManager.GetLogger(typeof(FoodItemRepository));

		private const string INVENTORY_FILE_PATH = @"D:\drans\Documents\Food Storage Inventory\inventory.txt";

		public static FoodItemRepository Instance => lazy.Value;

		private FoodItemRepository()
		{
			FoodItems = new ObservableCollection<FoodItem>()
			{
				new FoodItem(){Name = "Blackberry Jam", Quantity = 5, ContainerDescription = "Pint Jars"},
				new FoodItem(){Name = "Peaches Jam", Quantity = 3, ContainerDescription = "Quart Jars"},
			};
		}

		public ObservableCollection<FoodItem> FoodItems { get; set; }

		public bool SaveToFile()
		{
			try
			{
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
		}

		public bool BackupFile()
		{
		}
	}
}