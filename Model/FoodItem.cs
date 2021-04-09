﻿using System;

namespace Food_Storage_Inventory.Model
{
	public class FoodItem
	{
		private string _name { get; set; } = string.Empty;
		public string DisplayName { get => ToString(); }
		public int Quantity { get; set; }
		public string Container { get; set; } = string.Empty;
		public DateTime Date { get; set; } = DateTime.Now;
		public bool Visible { get; private set; }
		public string DisplayDate => Date.ToShortDateString();

		public string Overview => $"({Quantity}): {_name}, {Container}, {DisplayDate}";

		public override string ToString()
		{
			if (_name.Contains(Location.DEFAULT_FOOD_ITEM))
			{
				return _name;
			}

			return $"{_name} - {Container}";
		}

		public override bool Equals(object other)
		{
			if (!(other is FoodItem))
				return false;

			if (other is FoodItem item)
			{
				return DisplayName == item.DisplayName && Container == item.Container;
			}

			return false;
		}

		public override int GetHashCode()
		{
			var hashcode = 5649871;
			hashcode = hashcode * -5649977 + DisplayName.GetHashCode();
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