using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task InitializeAsync<TViewModel>(NavigationParameters parameters = null,bool navigationPage = false, NavigationPage customNavigationPage = null) where TViewModel : BaseViewModel
        => await InternalInitAsync(typeof(TViewModel), parameters, navigationPage, customNavigationPage);

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(NavigationParameters parameters) where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), parameters);

        public Task NavigateToAsync(Type viewModelType)
            => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, NavigationParameters parameters)
            => InternalNavigateToAsync(viewModelType, parameters);

        public async Task NavigateBackAsync(NavigationParameters parameters = null)
        {
            if (CurrentApplication.MainPage != null)
            {

                Xamarin.Forms.Page page;
                if (CurrentApplication.MainPage.Navigation.ModalStack.Count > 0)
                    page = await CurrentApplication.MainPage.Navigation.PopModalAsync();
                else
                    page = await CurrentApplication.MainPage.Navigation.PopAsync();

                if (parameters == null)
                {
                    parameters = new NavigationParameters();
                }

                parameters.NavigationState = NavigationState.Backward;

                page.AddNavigationArgs(parameters);
                var viewmodel = page.BindingContext as BaseViewModel;
                if (viewmodel != null)
                    await viewmodel.OnNavigate(parameters);

            }
        }

        public async Task NavigateAndClearBackStackAsync<TViewModel>(NavigationParameters parameters = null) where TViewModel : BaseViewModel
        {
            try
            {
                Xamarin.Forms.Page page = CreateAndBindPage(typeof(TViewModel), parameters);
                var navigationPage = CurrentApplication.MainPage as NavigationPage;

                await navigationPage.PushAsync(page);

                await (page.BindingContext as BaseViewModel).LoadAsync(parameters);

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

        async Task InternalNavigateToAsync(Type viewModelType, NavigationParameters parameters, bool modal = false)
        {
             Xamarin.Forms.Page page = CreateAndBindPage(viewModelType, parameters);

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

            await ParameterNavigation(page, parameters, NavigationState.Forward);
        }

        async Task InternalInitAsync(Type viewModelType, NavigationParameters parameters, bool navigationPage = false, NavigationPage customNavigationPage = null)
        {
            Xamarin.Forms.Page page = CreateAndBindPage(viewModelType, parameters);

            if (CurrentApplication.MainPage is NavigationPage currentNavigationPage)
            {
                await currentNavigationPage.PushAsync(page);
            }
            else
            {
                if (navigationPage)
                {
                    if(customNavigationPage != null)
                    {
                        CurrentApplication.MainPage = (NavigationPage)Activator.CreateInstance(customNavigationPage.GetType(), page);
                    }
                    else
                    {
                        CurrentApplication.MainPage = new NavigationPage(page);
                    }
                }
                else
                    CurrentApplication.MainPage = page;

            }

            await ParameterNavigation(page, parameters, NavigationState.Init);

        }

       

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!ViewModelLocator.Current.Mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return ViewModelLocator.Current.Mappings[viewModelType];
        }

        Xamarin.Forms.Page CreateAndBindPage(Type viewModelType, NavigationParameters parameters)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Xamarin.Forms.Page page = Activator.CreateInstance(pageType) as Xamarin.Forms.Page;
            try
            {
                BaseViewModel viewModel = ViewModelLocator.Current.Resolve(viewModelType) as BaseViewModel;
                page.BindingContext = viewModel;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"CreateAndBindPage error: {ex.Message}");
            }

            return page;
        }

        async Task ParameterNavigation(Xamarin.Forms.Page page,NavigationParameters parameters, NavigationState state)
        {
            if (parameters == null)
            {
                parameters = new NavigationParameters();
            }

            parameters.NavigationState = NavigationState.Init;

            page.AddNavigationArgs(parameters);

            await (page.BindingContext as BaseViewModel).LoadAsync(parameters);

            await (page.BindingContext as BaseViewModel).OnNavigate(parameters);
        }

    }
}
