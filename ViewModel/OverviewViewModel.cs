using System.Collections.Generic;
using System.Linq;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.ViewModel
{
	public class OverviewViewModel
	{
		public string Report => CompileReport();

		private string CompileReport()
		{
			HashSet<string> containers = new HashSet<string>();
			HashSet<FoodItem> foodItems = new HashSet<FoodItem>();

			int total = 0;

			foreach (Location location in LocationRepository.Instance.VisibleLocations)
			{
				foreach (FoodItem foodItem in location.ValidFoodItems)
				{
					containers.Add(foodItem.Container);
					foodItems.Add(foodItem);
					total += foodItem.Quantity;
				}
			}

			var usedLocations = LocationRepository.Instance.VisibleLocations.Where(x => x.ValidFoodItems.Any()).Count();

			return $"You have {foodItems.Count} food items stored in {containers.Count} types of containers in {usedLocations} locations.\nYou have {total} individual items";
		}
	}
}