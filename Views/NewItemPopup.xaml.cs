using System;
using System.Windows;
using System.Windows.Controls;

namespace Food_Storage_Inventory.Views
{
	/// <summary>
	/// Interaction logic for NewItemPopup.xaml
	/// </summary>
	public partial class NewItemPopup : Window
	{
		public NewItemPopup()
		{
			InitializeComponent();
			DisableFutureDates();
		}

		private void DisableFutureDates()
		{
			var tomorrow = DateTime.Now.AddDays(1);
			var forever = tomorrow.AddYears(1000);
			datePicker.SelectedDate = DateTime.Now;
			datePicker.BlackoutDates.Add(new CalendarDateRange(tomorrow, forever));
		}
	}
}