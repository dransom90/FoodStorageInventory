using System.Collections.ObjectModel;
using System.Linq;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.ViewModel
{
	public class OverviewViewModel
	{
		public ObservableCollection<Location> Locations { get; set; }

		public OverviewViewModel()
		{
			Locations = new ObservableCollection<Location>();
			CopyCollection();
		}

		private void CopyCollection()
		{
			foreach (Location location in LocationRepository.Instance.Locations.Where(x => x.Name != LocationRepository.DEFAULT_ENTRY))
			{
				var validFoodItems = location.StoredFoodItems.Where(x => x.Name != FoodItemRepository.DEFAULT_NAME);
				Location newLocation = new Location(location.Name) { StoredFoodItems = new ObservableCollection<FoodItem>(validFoodItems) };
				Locations.Add(newLocation);
			}
		}
	}
}