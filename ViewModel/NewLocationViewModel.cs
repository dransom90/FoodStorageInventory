using System.Linq;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Food_Storage_Inventory.Views;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class NewLocationViewModel
	{
		public string NewLocation { get; set; }
		public string Feedback { get; set; }
		public bool FeedbackVisible { get; set; }
		public ICommand OkCommand => new DelegateCommand<object>(OnOkCommand);

		private void OnOkCommand(object context)
		{
			if (NewLocation != null)
			{
				var nameAlreadyExists = LocationRepository.Instance.Locations.Select(x => x.Name).Any(y => y == NewLocation);

				if (nameAlreadyExists)
				{
					Feedback = "Location Already Exists";
					FeedbackVisible = true;
					return;
				}

				Location newLocation = new Location(NewLocation);
				bool success = LocationRepository.Instance.AddNewLocation(newLocation);

				if (success)
					LocationRepository.Instance.SelectedLocation = newLocation;

				if (context is NewLocationWindow locationWindow)
				{
					locationWindow.Close();
				}
			}
		}
	}
}