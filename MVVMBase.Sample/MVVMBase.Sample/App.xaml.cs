using MVVMBase.Sample.Controls;
using MVVMBase.Sample.Helpers;
using MVVMBase.Sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Microsoft.Extensions.DependencyInjection;
using MVVMBase.Sample.Services;

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
            Container.Current.RegisterForNavigation<MainPage, MainViewModel>();
            Container.Current.RegisterForNavigation<PokemonPage, PokemonViewModel>();

            Container.Current.Services.AddTransient<IPokemonService, PokemonService>();

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
