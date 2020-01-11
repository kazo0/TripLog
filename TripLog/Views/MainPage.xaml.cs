using System;
using System.Collections.Generic;
using System.Linq;
using TripLog.Models;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new MainViewModel();
		}

		void New_Clicked(System.Object sender, System.EventArgs e)
		{
			Navigation.PushAsync(new NewEntryPage()); 
		}

		async void Trips_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
		{
			var selection = (TripLogEntry)e.CurrentSelection.FirstOrDefault();
			if (selection != null)
			{
				await Navigation.PushAsync(new DetailPage(selection));
			}

			trips.SelectedItem = null;
		}
	}
}
