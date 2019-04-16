using System;
using System.Threading.Tasks;
using Xamarin.Forms.MVVMBase.ViewModels;


namespace Xamarin.Forms.MVVMBase.Services.Navigation
{
    public interface INavigationService
    {
        Task InitializeAsync<TViewModel>(NavigationParameters parameters = null,bool navigationPage = false, NavigationPage customNavigationPage = null) where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(NavigationParameters parameters) where TViewModel : BaseViewModel;
        Task NavigateToAsync(Type viewModelType);
        Task NavigateToAsync(Type viewModelType, NavigationParameters parameters);
        Task NavigateBackAsync(NavigationParameters parameters = null);
        Task NavigateAndClearBackStackAsync<TViewModel>(NavigationParameters parameters = null) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();
    }
}
