using System;
using MVVMBase.Sample.Controls;
using MVVMBase.Sample.Helpers;
using MVVMBase.Sample.Services;
using MVVMBase.Sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Xamarin.Forms.MVVMBase.ViewModels;
using Xamarin.Forms.Xaml;

namespace MVVMBase.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Constants.ApiURL = "https://pokeapi.co/api/v2";
            BuildDependencies();
            InitNavigation();
        }

        //Service Build
        public void BuildDependencies()
        {
            ViewModelLocator.Current.RegisterForNavigation<MainPage, MainViewModel>();
            ViewModelLocator.Current.RegisterForNavigation<PokemonPage, PokemonViewModel>();
        }

        async void InitNavigation()
        {
            var navigationService = ViewModelLocator.Current.Resolve<INavigationService>();
            ViewModelLocator.Current.Register<IPokemonService, PokemonService>();


            //Basic Startup
            //await navigationService.InitializeAsync<MainViewModel>(null, true);

            //Custom Navigation
            await navigationService.InitializeAsync<MainViewModel>(null, true, new CustomNavigation());

            //CustomNavigation
        }
    }
}
