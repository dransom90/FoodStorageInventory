﻿using System;

namespace Food_Storage_Inventory.Model
{
	public class FoodItem
	{
		private string _name { get; set; }
		public int Quantity { get; set; }
		public string Container { get; set; }
		public string Name { get => ToString(); }

		public override string ToString() => $"{_name} - {Container}";

		public FoodItem(string name, int quantity, string container)
		{
			_name = name ?? throw new ArgumentNullException(nameof(name));
			Quantity = quantity;
			Container = container ?? throw new ArgumentNullException(nameof(container));
		}
	}
}