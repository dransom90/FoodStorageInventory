using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.Views
{
	/// <summary>
	/// Interaction logic for EditItems.xaml
	/// </summary>
	public partial class EditItems : Page
	{
		public EditItems()
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