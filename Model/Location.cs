using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Storage_Inventory.Model
{
	public class Location
	{
		public string Name { get; set; }
		public List<string> StoredFoodItems { get; set; }

		public Location(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			StoredFoodItems = new List<string>();
		}

		public override string ToString()
		{
			return Name;
		}

		//TODO:	Implement Add and Remove logic
	}
}