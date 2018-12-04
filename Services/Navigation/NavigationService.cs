using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase.ViewModels;

namespace Xamarin.Forms.MVVMBase.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public NavigationService()
        {

        }

        public async Task InitializeAsync<TViewModel>(object parameter = null,bool navigationPage = false) where TViewModel : BaseViewModel
        => await InternalInitAsync(typeof(TViewModel), parameter, navigationPage);

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), parameter);

        public Task NavigateToAsync(Type viewModelType)
            => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, object parameter)
            => InternalNavigateToAsync(viewModelType, parameter);

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public async Task NavigateAndClearBackStackAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel
        {
            try
            {
                Page page = CreateAndBindPage(typeof(TViewModel), parameter);
                var navigationPage = CurrentApplication.MainPage as NavigationPage;

                await navigationPage.PushAsync(page);

                await (page.BindingContext as BaseViewModel).LoadAsync(parameter);

                if (navigationPage != null && navigationPage.Navigation.NavigationStack.Count > 0)
                {
                    var existingPages = navigationPage.Navigation.NavigationStack.ToList();

                    foreach (var existingPage in existingPages)
                    {
                        if (existingPage != page)
                            navigationPage.Navigation.RemovePage(existingPage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"NavigateAndClearBackStackAsync: {ex.Message}");
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }

        async Task InternalNavigateToAsync(Type viewModelType, object parameter, bool modal = false)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            var currentNavigationPage = CurrentApplication.MainPage as NavigationPage;

            if (currentNavigationPage != null)
            {
            
                if (modal)
                    await CurrentApplication.MainPage.Navigation.PushModalAsync(page);
                else
                    await currentNavigationPage.PushAsync(page);
            }
            else
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }

            await (page.BindingContext as BaseViewModel).LoadAsync(parameter);
        }

        async Task InternalInitAsync(Type viewModelType, object parameter, bool navigationPage = false)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            var currentNavigationPage = CurrentApplication.MainPage as NavigationPage;

            if (currentNavigationPage != null)
            {
                await currentNavigationPage.PushAsync(page);
            }
            else
            {
                if (navigationPage)
                    CurrentApplication.MainPage = new NavigationPage(page);
                else
                    CurrentApplication.MainPage = page;

            }

            await (page.BindingContext as BaseViewModel).LoadAsync(parameter);

        }

       

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!ViewModelLocator.Current.Mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return ViewModelLocator.Current.Mappings[viewModelType];
        }

        Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            BaseViewModel viewModel = ViewModelLocator.Current.Resolve(viewModelType) as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }

    }
}
