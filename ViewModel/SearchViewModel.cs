using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.ViewModel
{
	public class SearchViewModel
	{
		private string searchText;

		public ObservableCollection<Location> SearchResults { get; set; } = new ObservableCollection<Location>();

		public string SearchText
		{
			get => searchText;
			set
			{
				if (searchText == value)
					return;
				searchText = value;
				UpdateSearchResults();
			}
		}

		private void UpdateSearchResults()
		{
			var searchResults = new List<FoodItem>();
			var lowerText = SearchText.ToLowerInvariant();

			SearchResults.Clear();

			foreach (Location location in LocationRepository.Instance.Locations.Where(x => x.Name != LocationRepository.DEFAULT_ENTRY))
			{
				var validFoodItems = location.StoredFoodItems.Where(x => x.Name.ToLowerInvariant().Contains(lowerText) && x.Name != FoodItemRepository.DEFAULT_NAME);
				Location newLocation = new Location(location.Name) { StoredFoodItems = new ObservableCollection<FoodItem>(validFoodItems) };
				SearchResults.Add(newLocation);
			}
		}
	}
}