using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Food_Storage_Inventory.Views
{
	/// <summary>
	/// Interaction logic for HomePage.xaml
	/// </summary>
	public partial class HomePage : Page
	{
		public HomePage()
		{
			InitializeComponent();
		}

		private void OverviewClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ReportsPage());

		private void SearchClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new SearchWindow());

		private void InventoryClick(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("This will reset all Items back to zero!  Do you want to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
			if (result == MessageBoxResult.Yes)
			{
				NavigationService.Navigate(new GeneralInventoryWindow());
			}
		}

		private void EditItems(object sender, RoutedEventArgs e) => NavigationService.Navigate(new EditItems());

		private void AboutClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new About());
		}

		private void DescriptionClick(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new Descriptions());
		}
	}
}