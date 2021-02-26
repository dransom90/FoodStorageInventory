using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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