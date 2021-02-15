using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Food_Storage_Inventory.Model
{
	public sealed class FoodItemRepository
	{
		private static readonly Lazy<FoodItemRepository> lazy = new Lazy<FoodItemRepository>(() => new FoodItemRepository());

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
	}
}