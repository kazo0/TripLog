using System;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Essentials;

namespace TripLog.iOS.Services
{
	public class LocationService : ILocationService
	{
		public async Task<GeoCoords> GetGeoCoordinates()
		{
			var location = await Geolocation.GetLocationAsync();

			return new GeoCoords
			{
				Latitude = location.Latitude,
				Longitude = location.Longitude,
			};
		}
	}
}
