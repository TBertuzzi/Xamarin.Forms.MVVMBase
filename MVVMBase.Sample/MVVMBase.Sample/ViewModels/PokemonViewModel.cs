using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase.Sample.Models;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase.Extensions;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Xamarin.Forms.MVVMBase.ViewModels;

namespace MVVMBase.Sample.ViewModels
{
    public class PokemonViewModel : BaseViewModel
    {
        public ICommand BackCommand { get; }

        private Pokemon _pokemon;
        public Pokemon Pokemon
        {
            get { return _pokemon; }
            set
            {
                SetProperty(ref _pokemon, value);
            }
        }

        public PokemonViewModel() : base("Pokemon View")
        {
            BackCommand = new Command(ExecuteBackCommand);
            Pokemon = new Pokemon();
        }

        private async void ExecuteBackCommand(object obj)
        {
            await NavigationService.NavigateBackAsync();
        }

        public override async Task LoadAsync(NavigationParameters navigationData)
        {
            try
            {
                if (navigationData != null &&
                    navigationData.ContainsKey("pokemon"))
                {
                    //using Object Extension to Easy Cast Object
                    Pokemon = navigationData["pokemon"].Cast<Pokemon>();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
