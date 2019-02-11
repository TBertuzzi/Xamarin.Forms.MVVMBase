using System;
using System.Threading.Tasks;
using Xamarin.Forms.MVVMBase.ViewModels;


namespace Xamarin.Forms.MVVMBase.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync<TViewModel>(object parameter = null,bool navigationPage = false, NavigationPage customNavigationPage = null) where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task NavigateToAsync(Type viewModelType);
        Task NavigateToAsync(Type viewModelType, object parameter);
        Task NavigateBackAsync();
        Task NavigateAndClearBackStackAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();
    }
}
