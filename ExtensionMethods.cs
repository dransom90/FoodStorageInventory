using System.Collections.ObjectModel;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory
{
	public static class ExtensionMethods
	{
		public static void ResetQuantities(this ObservableCollection<FoodItem> collection)
		{
			foreach (FoodItem foodItem in collection)
			{
				foodItem.Quantity = 0;
			}
		}
	}
}