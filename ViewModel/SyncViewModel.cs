using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using Food_Storage_Inventory.Model;
using Prism.Commands;

namespace Food_Storage_Inventory.ViewModel
{
	public class SyncViewModel : INotifyPropertyChanged
	{
		private string _downloadFeedback;
		private bool _downloadFeedbackVisible;
		private string _uploadFeedback;
		private bool _uploadFeedbackVisible;
		private bool _downloadOptionsVisible;
		private bool _uploadOptionsVisible;

		public event PropertyChangedEventHandler PropertyChanged;

		private Timer _visibilityTimer;

		public string DownloadFeedback
		{
			get => _downloadFeedback;
			set
			{
				if (_downloadFeedback == value)
					return;
				_downloadFeedback = value;
				NotifyPropertyChanged(nameof(DownloadFeedback));
			}
		}

		public string UploadFeedback
		{
			get => _uploadFeedback;
			set
			{
				if (_uploadFeedback == value)
					return;
				_uploadFeedback = value;
				NotifyPropertyChanged(nameof(UploadFeedback));
			}
		}

		public bool DownloadFeedbackVisible
		{
			get => _downloadFeedbackVisible;
			set
			{
				if (_downloadFeedbackVisible == value)
					return;
				_downloadFeedbackVisible = value;
				NotifyPropertyChanged(nameof(DownloadFeedbackVisible));
			}
		}

		public bool UploadFeedbackVisible
		{
			get => _uploadFeedbackVisible;
			set
			{
				if (_uploadFeedbackVisible == value)
					return;
				_uploadFeedbackVisible = value;
				NotifyPropertyChanged(nameof(UploadFeedbackVisible));
			}
		}

		public bool DownloadOptionsVisible
		{
			get => _downloadOptionsVisible;
			set
			{
				if (_downloadOptionsVisible == value)
					return;
				_downloadOptionsVisible = value;
				NotifyPropertyChanged(nameof(DownloadOptionsVisible));
			}
		}

		public bool UploadOptionsVisible
		{
			get => _uploadOptionsVisible;
			set
			{
				if (_uploadOptionsVisible == value)
					return;
				_uploadOptionsVisible = value;
				NotifyPropertyChanged(nameof(UploadOptionsVisible));
			}
		}

		public ICommand UploadCommand => new DelegateCommand(OnUpload);
		public ICommand DownloadCommand => new DelegateCommand(OnDownload);
		public ICommand ContinueCommand => new DelegateCommand(OnContinue);

		private void OnUpload()
		{
			UploadFeedback = "Contacting Server.  Please Wait...";
			UploadFeedbackVisible = true;

			bool success = LocationRepository.Instance.BackupLocations();
			if (success)
			{
				UploadFeedback = "Backup Successful";
			}
			else
			{
				UploadFeedback = "Backup Failed.";
			}

			SetTimer();
		}

		private void OnDownload()
		{
			DownloadFeedback = "Contacting Server.  Please Wait...";
			DownloadFeedbackVisible = true;

			bool success = LocationRepository.Instance.LoadFromBackup();
			if (success)
			{
				DownloadFeedback = "Download Successful";
				DownloadOptionsVisible = false;
			}
			else
			{
				DownloadFeedback = "Download Failed.";
				DownloadOptionsVisible = true;
			}

			SetTimer();
		}

		private void OnContinue()
		{
			LocationRepository.Instance.ReadFromFile();
		}

		private void SetTimer()
		{
			_visibilityTimer = new Timer(5000);
			_visibilityTimer.Elapsed += VisibilityElapsed;
			_visibilityTimer.AutoReset = false;
			_visibilityTimer.Enabled = true;
		}

		private void VisibilityElapsed(object sender, ElapsedEventArgs e)
		{
			DownloadFeedbackVisible = false;
			UploadFeedbackVisible = false;
		}

		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}