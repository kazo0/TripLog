using System;
using System.Collections.Generic;
using TripLog.ViewModels;
using Xamarin.Forms;
using System.Linq;
using TripLog.Services;

namespace TripLog.Views
{
	public partial class NewEntryPage : ContentPage
	{
		private NewEntryViewModel ViewModel => BindingContext as NewEntryViewModel;

		public NewEntryPage()
		{
			InitializeComponent();
			BindingContextChanged += NewEntryPage_BindingContextChanged;
		}

		private void NewEntryPage_BindingContextChanged(object sender, EventArgs e)
		{
			ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
		}

		private void ViewModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
		{
			var propErrors = ViewModel.GetErrors(e.PropertyName) as List<string>;
			var hasErrors = propErrors.Any();

			switch (e.PropertyName)
			{
				case nameof(ViewModel.Title):
					title.LabelColor = hasErrors ? Color.Red : Color.Black;
					break;
				case nameof(ViewModel.Rating):
					rating.LabelColor = hasErrors ? Color.Red : Color.Black;
					break;
				default:
					break;
			}
		}
	}
}
