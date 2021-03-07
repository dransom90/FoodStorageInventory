using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using log4net;
using Newtonsoft.Json;

namespace Food_Storage_Inventory.Model
{
	public class LocationRepository : INotifyPropertyChanged
	{
		private static readonly Lazy<LocationRepository> lazy = new Lazy<LocationRepository>(() => new LocationRepository());
		private static readonly ILog _logger = LogManager.GetLogger(typeof(LocationRepository));

		private const string DIRECTORY = @"C:\Program Files\Food Storage Inventory";
		private const string FILE_PATH = @"C:\Program Files\Food Storage Inventory\locations.json";
		public const string DEFAULT_ENTRY = "Create A New Location";

		public static LocationRepository Instance = lazy.Value;
		private Location selectedLocation;
		private FoodItem selectedFoodItem;

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<Location> Locations { get; set; }
		public ObservableCollection<Location> VisibleLocations { get => new ObservableCollection<Location>(Locations.Where(x => x.Visible)); }
		public Location CurrentLocation { get; set; }

		public Location SelectedLocation
		{
			get => selectedLocation;
			set
			{
				if (selectedLocation == value)
					return;
				selectedLocation = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedLocation)));
			}
		}

		public FoodItem SelectedFoodItem
		{
			get => selectedFoodItem;
			set
			{
				if (selectedFoodItem == value)
					return;
				selectedFoodItem = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFoodItem)));
			}
		}

		private LocationRepository()
		{
			_ = ReadFromFile();
		}

		public void NotifyVisibleItemsUpdated()
		{
			NotifyPropertyChanged(nameof(VisibleLocations));
		}

		public void ResetAllQuantities()
		{
			foreach (Location location in Locations)
			{
				location.ResetAllQuantities();
			}
		}

		public bool AddNewLocation(Location location)
		{
			if (Locations.Contains(location))
				return false;

			Locations.Add(location);
			SaveToFile();

			return true;
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
					new Location(DEFAULT_ENTRY, false),
					new Location("None", true)
				};
			}
			return false;
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}