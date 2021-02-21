using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using log4net;
using Newtonsoft.Json;

namespace Food_Storage_Inventory.Model
{
	public class LocationRepository
	{
		private static readonly Lazy<LocationRepository> lazy = new Lazy<LocationRepository>(() => new LocationRepository());
		private static readonly ILog _logger = LogManager.GetLogger(typeof(LocationRepository));

		private const string DIRECTORY = @"C:\Program Files\Food Storage Inventory";
		private const string FILE_PATH = @"C:\Program Files\Food Storage Inventory\locations.txt";
		private const string DEFAULT_ENTRY = "Create A New Location";

		public static LocationRepository Instance = lazy.Value;

		public ObservableCollection<Location> Locations { get; set; }

		private LocationRepository()
		{
			_ = ReadFromFile();
		}

		public bool SaveToFile()
		{
			try
			{
				if (!Directory.Exists(DIRECTORY))
				{
					Directory.CreateDirectory(DIRECTORY);
				}

				using (StreamWriter file = File.CreateText(FILE_PATH))
				{
					JsonSerializer jsonSerializer = new JsonSerializer();
					var json = JsonConvert.SerializeObject(Locations);
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
				using (StreamReader file = new StreamReader(FILE_PATH))
				{
					var json = file.ReadToEnd();

					Locations = JsonConvert.DeserializeObject<ObservableCollection<Location>>(json);
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.Error("Failed to read JSON file.  Creating empty collection.", ex);
				Locations = new ObservableCollection<Location>()
				{
					new Location(DEFAULT_ENTRY),
					new Location("None")
				};
			}
			return false;
		}
	}
}