using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Food_Storage_Inventory.Views;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class MainMenuViewModel
	{
		public ICommand ExitProgramCommand => new DelegateCommand<object>(OnExitProgram);
		public ICommand SaveCommand => new DelegateCommand<object>(OnSaveExecuted);
		public ICommand BackUpCommand => new DelegateCommand<object>(OnBackupExecuted);
		public ICommand OverviewCommand => new DelegateCommand<object>(OnOverviewExecuted);
		public ICommand SearchCommand => new DelegateCommand<object>(OnSearchExecuted);
		public ICommand InventoryCommand => new DelegateCommand<object>(OnInventoryExecuted);

		private void OnExitProgram(object context) => Application.Current.Shutdown();

		private void OnSaveExecuted(object context) => FoodItemRepository.Instance.SaveToFile();

		private void OnBackupExecuted(object context) => FoodItemRepository.Instance.BackupFile();

		private void OnOverviewExecuted(object context)
		{
		}

		private void OnSearchExecuted(object context)
		{
		}

		private void OnInventoryExecuted(object context)
		{
			GeneralInventoryWindow generalInventoryWindow = new GeneralInventoryWindow();
			generalInventoryWindow.Show();
		}
	}
}