﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using log4net;
using Newtonsoft.Json;

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
			_ = ReadFromFile();
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
			try
			{
				using (StreamReader file = new StreamReader(INVENTORY_FILE_PATH))
				{
					var json = file.ReadToEnd();

					FoodItems = JsonConvert.DeserializeObject<ObservableCollection<FoodItem>>(json);
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error("Failed to read JSON file.  Creating empty collection.", ex);
				FoodItems = new ObservableCollection<FoodItem>();
				return false;
			}
		}

		public bool BackupFile()
		{
			return false;
		}
	}
}