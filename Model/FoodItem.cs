namespace Food_Storage_Inventory.Model
{
	public class FoodItem
	{
		public string Name { get; set; }
		public int Quantity { get; set; }

		public override string ToString() => Name;
	}
}