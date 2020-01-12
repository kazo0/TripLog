using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Ninject;
using TripLog.Extensions;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TripLog.Services
{
	public class NavigationService : INavService
	{
		public INavigation Navigation { get; set; }

		public bool CanGoBack => Navigation.NavigationStack?.Count > 0;

		private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();

		public NavigationService()
		{
		}

		public void RegisterViewMapping(Type viewModel, Type view)
		{
			_map.Add(viewModel, view);
		}

		public void OnCanGoBackChanged()
		{
			CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanGoBack)));
		}

		public event PropertyChangedEventHandler CanGoBackChanged;

		public async Task GoBack()
		{
			if (!CanGoBack)
			{
				return;
			}

			await Navigation.PopAsync();
			OnCanGoBackChanged();
		}

		public async Task NavigateTo<TVM>() where TVM : BaseViewModel
		{
			await NavigateToView(typeof(TVM));

			if (Navigation.NavigationStack.Last().BindingContext is BaseViewModel baseViewModel)
			{
				baseViewModel.Init();
			}
		}

		public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
		{
			await NavigateToView(typeof(TVM));

			if (Navigation.NavigationStack.Last().BindingContext is BaseViewModel<TParameter> baseViewModel)
			{
				baseViewModel.Init(parameter);
			}
		}

		public void RemoveLastView()
		{
			if (Navigation.NavigationStack.Count < 2)
			{
				return;
			}
			var lastView = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
			Navigation.RemovePage(lastView);

			OnCanGoBackChanged();
		}

		public void ClearBackStack()
		{
			if (Navigation.NavigationStack.Count < 2)
			{
				return;
			}

			for (var i = 0; i < Navigation.NavigationStack.Count - 1; i++)
			{
				Navigation.RemovePage(Navigation.NavigationStack[i]);
			}

			OnCanGoBackChanged();
		}

		public async Task NavigateToUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentException("Invalid URI");
			}

			await Launcher.OpenAsync(uri);
		}

		private async Task NavigateToView(Type viewModel)
		{
			if (!_map.TryGetValue(viewModel, out var viewType))
			{
				throw new ArgumentException($"No view found in view mapping for {viewModel.FullName}.");
			}

			var constructor = viewType.GetTypeInfo()
				.DeclaredConstructors
				.FirstOrDefault(c => c.GetParameters().None());

			if (constructor == null)
			{
				throw new InvalidOperationException($"Missing parameterless constructor for view type {viewType.FullName}.");
			}

			var view = constructor.Invoke(null) as Page;
			view.BindingContext = ((App)Application.Current).Kernel
				.GetService(viewModel);

			await Navigation.PushAsync(view, true);
		}
	}
}
