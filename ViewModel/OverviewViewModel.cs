using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.ViewModel
{
	public class OverviewViewModel
	{
		public string ZeroItemReport { get; set; }
		public string LowItemReport { get; set; }
		public string NoReports { get; set; }
		public bool DisplayNoReports => string.IsNullOrEmpty(NoReports);
		public bool DisplayFiveOrLess => FiveOrLess.Any();
		public bool DisplayZeroItems => ZeroItems.Any();
		public ObservableCollection<string> ZeroItems { get; set; }
		public ObservableCollection<string> FiveOrLess { get; set; }

		public OverviewViewModel()
		{
			ZeroItems = new ObservableCollection<string>();
			FiveOrLess = new ObservableCollection<string>();
			CompileReport();
		}

		private void CompileReport()
		{
			List<string> emptyItems = new List<string>();
			List<string> lowItems = new List<string>();

			int empty = 0;

			foreach (Location location in LocationRepository.Instance.VisibleLocations)
			{
				foreach (FoodItem foodItem in location.ValidFoodItems)
				{
					if (foodItem.Quantity == 0)
					{
						emptyItems.Add($"{foodItem} ({location})");
						empty++;
					}
					else if (foodItem.Quantity <= 5)
					{
						lowItems.Add($"{foodItem} ({location})");
					}
				}
			}

			if (emptyItems.Any())
			{
				ZeroItemReport = $"You have used up the following items:";
				foreach (string item in emptyItems)
				{
					ZeroItems.Add(item);
				}
			}
			else
			{
				ZeroItemReport = string.Empty;
			}

			if (lowItems.Any())
			{
				LowItemReport = "You have 5 or less of the following items:";
				foreach (string item in lowItems)
				{
					FiveOrLess.Add(item);
				}
			}
			else
			{
				LowItemReport = string.Empty;
			}

			if (string.IsNullOrEmpty(ZeroItemReport) && string.IsNullOrEmpty(LowItemReport))
				NoReports = "Nothing to report today!";
		}
	}
}