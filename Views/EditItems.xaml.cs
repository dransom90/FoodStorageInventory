using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Food_Storage_Inventory.Model;

namespace Food_Storage_Inventory.Views
{
	/// <summary>
	/// Interaction logic for EditItems.xaml
	/// </summary>
	public partial class EditItems : Page
	{
		private FoodItem selectedItem;

		public EditItems()
		{
			InitializeComponent();
		}

		private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (treeView.SelectedItem is FoodItem foodItem)
			{
				selectedItem = foodItem;
			}
		}

		private void PlusButtonClick(object sender, RoutedEventArgs e)
		{
			if (treeView.SelectedItem is FoodItem foodItem)
			{
				foodItem.Quantity++;
				LocationRepository.Instance.NotifyVisibleItemsUpdated();
			}
		}

		private void MinusButtonClick(object sender, RoutedEventArgs e)
		{
			if (treeView.SelectedItem is FoodItem foodItem)
			{
				foodItem.Quantity--;
				LocationRepository.Instance.NotifyVisibleItemsUpdated();
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}

		private void UIntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
		}

		private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
		{
			if (sender is StackPanel stackPanel)
			{
				stackPanel.Focus();
			}
		}
	}
}