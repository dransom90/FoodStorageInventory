using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Food_Storage_Inventory.Model
{
	public class Location : INotifyPropertyChanged
	{
		public static readonly string DEFAULT_FOOD_ITEM = "Create A New Item";

		public event PropertyChangedEventHandler PropertyChanged;

		public string Name { get; set; }
		public ObservableCollection<FoodItem> StoredFoodItems { get; set; }
		public ObservableCollection<FoodItem> ValidFoodItems { get => new ObservableCollection<FoodItem>(StoredFoodItems.Where(x => x.Visible)); }

		public bool Visible { get; set; }

		public Location(string name, bool visible)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			Visible = visible;
			CheckForDefaultEntry();
		}

		private void CheckForDefaultEntry()
		{
			if (StoredFoodItems is null)
			{
				StoredFoodItems = new ObservableCollection<FoodItem>()
				{
					new FoodItem(DEFAULT_FOOD_ITEM, 0, "NONE", DateTime.Now, false)
				};
			}

			var names = StoredFoodItems.Where(x => x.Name == DEFAULT_FOOD_ITEM);

			if (!names.Any())
			{
				StoredFoodItems.Add(new FoodItem(DEFAULT_FOOD_ITEM, 0, "NONE", DateTime.Now, false));
			}
		}

		public void ResetAllQuantities()
		{
			StoredFoodItems.ResetQuantities();
		}

		public bool AddItem(FoodItem foodItem)
		{
			if (StoredFoodItems.Contains(foodItem))
			{
				return false;
			}

			StoredFoodItems.Add(foodItem);
			return true;
		}

		public bool UpdateItem(FoodItem foodItem, int quantity)
		{
			if (StoredFoodItems.Contains(foodItem))
			{
				foodItem.Quantity = quantity;
				return true;
			}

			return false;
		}

		public override string ToString()
		{
			return Name;
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		//TODO:	Implement Add and Remove logic
	}
}