using System;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
	public class NewEntryViewModel : BaseViewModel
	{
		#region Properties
		private string _title;
		public string Title {
			get => _title;
			set
			{
				_title = value;
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
				OnPropertyChanged();
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
		public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save, CanSave));

		#endregion

		public NewEntryViewModel()
		{
			Date = DateTime.Today;
			Rating = 1;
		}

		private void Save()
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

			// TODO: Persist entry in a later chapter
		}

		private bool CanSave()
		{
			return !string.IsNullOrEmpty(Title);
		}
	}
}
