using System;
using System.ComponentModel;
using System.Threading.Tasks;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Services
{
    public interface INavService
    {
        bool CanGoBack { get; }
        INavigation Navigation { get; set; }
        Task GoBack();
        Task NavigateTo<TVM>() where TVM : BaseViewModel;
        Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;
        void RemoveLastView();
        void ClearBackStack();
        Task NavigateToUri(Uri uri);
        void RegisterViewMapping(Type viewModel, Type view);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
