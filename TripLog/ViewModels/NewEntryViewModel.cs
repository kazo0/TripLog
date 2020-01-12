using System;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
	public class NewEntryViewModel : BaseValidationViewModel
	{
		#region Properties
		private string _title;
		public string Title {
			get => _title;
			set
			{
				_title = value;
				Validate(
					() => !string.IsNullOrWhiteSpace(_title),
					"Title must be provided"
				);
				OnPropertyChanged();
				SaveCommand.ChangeCanExecute();
			}
		}

		private DateTime _date;
		public DateTime Date
		{
			get => _date;
			set
			{
				_date = value;
				OnPropertyChanged();
			}
		}

		private double _latitude;
		public double Latitude
		{
			get => _latitude;
			set
			{
				_latitude = value;
				OnPropertyChanged();
			}
		}

		private double _longitude;
		public double Longitude
		{
			get => _longitude;
			set
			{
				_longitude = value;
				OnPropertyChanged();
			}
		}

		private int _rating;
		public int Rating
		{
			get => _rating;
			set
			{
				_rating = value;
				Validate(
					() => _rating >= 1 && _rating <= 5,
					"Rating must be between 1 and 5");
				OnPropertyChanged();
				SaveCommand.ChangeCanExecute();
			}
		}

		private string _notes;
		public string Notes
		{
			get => _notes;
			set
			{
				_notes = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region Commands

		private Command _saveCommand;
		public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

		#endregion

		private readonly ILocationService _locationService;

		public NewEntryViewModel(INavService navService, ILocationService locationService) : base(navService)
		{
			Date = DateTime.Today;
			Rating = 1;

			_locationService = locationService;
		}

		public override async void Init()
		{
			try
			{
				var coords = await _locationService.GetGeoCoordinates();
				Latitude = coords.Latitude;
				Longitude = coords.Longitude;
			}
			catch (Exception e)
			{
				//TODO Handle Exception
			}
		}

		private async Task Save()
		{
			var newItem = new TripLogEntry
			{
				Title = Title,
				Latitude = Latitude,
				Longitude = Longitude,
				Date = Date,
				Rating = Rating,
				Notes = Notes
			};

			await NavService.GoBack();
		}

		private bool CanSave()
		{
			return !string.IsNullOrEmpty(Title) && !HasErrors;
		}
	}
}
