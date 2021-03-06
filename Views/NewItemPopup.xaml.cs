﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

			datePicker.BlackoutDates.Add(new CalendarDateRange(tomorrow, forever));
		}
	}
}