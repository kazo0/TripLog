using System;
using System.Globalization;
using Xamarin.Forms;

namespace TripLog.Converters
{
	public class RatingToStarImageNameConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			const string starsPrefix = "stars_";

			if (value is int rating)
			{
				if (rating <= 1)
				{
					rating = 1;
				}

				if (rating >= 5)
				{
					rating = 5;
				}

				return $"{starsPrefix}{rating}";
			}

			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
