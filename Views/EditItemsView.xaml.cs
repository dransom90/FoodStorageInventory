using System.Windows;
using System.Windows.Controls;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.Views
{
	/// <summary>
	/// Interaction logic for EditItems.xaml
	/// </summary>
	public partial class EditItemsView : UserControl
	{
		public EditItemsView()
		{
			InitializeComponent();
		}

		private void UIntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			LocationRepository.Instance.SaveToFile();
		}

		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			LocationRepository.Instance.SaveToFile();
		}
	}
}