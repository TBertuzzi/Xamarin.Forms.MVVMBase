﻿using MVVMBase.Sample.Controls;
using MVVMBase.Sample.Helpers;
using MVVMBase.Sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using MVVMBase.Sample.Services;
using Xamarin.Essentials;
using System.IO;

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
        private void BuildDependencies()
        {
            Container.Current.RegisterForNavigation<MainPage, MainViewModel>();
            Container.Current.RegisterForNavigation<PokemonPage, PokemonViewModel>();

            //Register services for use
            Container.Current.Register<IPokemonService, PokemonService>(LifeTime.Singleton);

            //Configure Container
            Container.Current.Setup();
        }

        async void InitNavigation()
        {
            var navigationService = Container.Current.Resolve<INavigationService>();
           
            //Basic Startup
            //await navigationService.InitializeAsync<MainViewModel>(null, true);

            //Custom Navigation
            await navigationService.InitializeAsync<MainViewModel>(null, true, new CustomNavigation());
        }
    }
}
