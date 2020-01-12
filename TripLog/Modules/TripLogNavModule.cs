using System;
using Ninject.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using TripLog.Extensions;
using Ninject;

namespace TripLog.Modules
{
	public class TripLogNavModule : NinjectModule
	{
		public override void Load()
		{
			Bind<INavService>()
				.To<NavigationService>()
				.InSingletonScope();

			var navService = Kernel.Get<INavService>();

			navService.RegisterViewMapping(typeof(DetailViewModel), typeof(DetailPage));
			navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
			navService.RegisterViewMapping(typeof(NewEntryViewModel), typeof(NewEntryPage));
		}
	}
}
