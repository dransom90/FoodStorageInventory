﻿using System;

namespace Food_Storage_Inventory.Model
{
	public class FoodItem
	{
		private string _name { get; set; }
		public string Name { get => ToString(); }
		public int Quantity { get; set; }
		public string Container { get; set; }
		public DateTime Date { get; set; }
		public bool Visible { get; private set; }
		public string DisplayDate => Date.ToShortDateString();

		public string Overview => $"({Quantity}): {_name}, {Container}, {DisplayDate}";

		public override string ToString() => $"{_name}";

		public override bool Equals(object other)
		{
			if (!(other is FoodItem))
				return false;

			if (other is FoodItem item)
			{
				return Name == item.Name && Container == item.Container;
			}

			return false;
		}

		public override int GetHashCode()
		{
			var hashcode = 5649871;
			hashcode = hashcode * -5649977 + Name.GetHashCode();
			hashcode = hashcode * -5649977 + Container.GetHashCode();
			return hashcode;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="name">The name of the item</param>
		/// <param name="quantity">How many currently exist?</param>
		/// <param name="container">Describe the container</param>
		/// <param name="visible">Should this be visible in searches?</param>
		public FoodItem(string name, int quantity, string container, DateTime dateTime, bool visible)
		{
			_name = name ?? throw new ArgumentNullException(nameof(name));
			Quantity = quantity;
			Container = container ?? throw new ArgumentNullException(nameof(container));
			Date = dateTime;
			Visible = visible;
		}
	}
}