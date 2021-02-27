using System.Windows;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class MainMenuViewModel
	{
		public ICommand ExitProgramCommand => new DelegateCommand<object>(OnExitProgram);
		public ICommand SaveCommand => new DelegateCommand<object>(OnSaveExecuted);
		public ICommand BackUpCommand => new DelegateCommand<object>(OnBackupExecuted);

		private void OnExitProgram(object context) => Application.Current.Shutdown();

		private void OnSaveExecuted(object context) => LocationRepository.Instance.SaveToFile();

		private void OnBackupExecuted(object context) => FoodItemRepository.Instance.BackupFile();
	}
}