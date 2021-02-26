﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Storage_Inventory.Model
{
	public class Location
	{
		public static readonly string DEFAULT_FOOD_ITEM = "Create A New Item";
		public string Name { get; set; }
		public ObservableCollection<FoodItem> StoredFoodItems { get; set; }

		public Location(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			CheckForDefaultEntry();
		}

		private void CheckForDefaultEntry()
		{
			if (StoredFoodItems is null)
			{
				StoredFoodItems = new ObservableCollection<FoodItem>()
				{
					new FoodItem(DEFAULT_FOOD_ITEM, 0, "NONE")
				};
			}

			var names = StoredFoodItems.Where(x => x.Name == DEFAULT_FOOD_ITEM);

			if (!names.Any())
			{
				StoredFoodItems.Add(new FoodItem(DEFAULT_FOOD_ITEM, 0, "NONE"));
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

		//TODO:	Implement Add and Remove logic
	}
}