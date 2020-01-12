using System;
using Ninject;
using Ninject.Modules;
using TripLog.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripLog
{
	public partial class App : Application
	{
		public IKernel Kernel { get; set; }
		public App(params INinjectModule[] platformModules)
		{
			InitializeComponent();

			Kernel = new StandardKernel(
				new TripLogCoreModule(),
				new TripLogNavModule());
			Kernel.Load(platformModules);

			SetMainPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}

		private void SetMainPage()
		{
			var mainPage = new NavigationPage(new MainPage
			{
				BindingContext = Kernel.Get<MainViewModel>()
			});

			var navService = Kernel.Get<INavService>();
			navService.Navigation = mainPage.Navigation;

			MainPage = mainPage;
		}
	}
}
