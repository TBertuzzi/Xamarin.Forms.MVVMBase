using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVMBase.Sample.Helpers;
using MVVMBase.Sample.Models;
using MVVMBase.Sample.Services;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Xamarin.Forms.MVVMBase.ViewModels;

namespace MVVMBase.Sample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Pokemon> Pokemons { get; }
        IPokemonService _PokemonService;

        private ICommand _itemTappedCommand;
        public ICommand ItemTappedCommand => _itemTappedCommand ?? (_itemTappedCommand =
            new Command<Pokemon>(async (pokemon) => await ItemTappedCommandExecute(pokemon), (pokemon) => !IsBusy));

        public MainViewModel(IPokemonService pokemonService) : base("Main View")
        {
            Pokemons = new ObservableCollection<Pokemon>();
            _PokemonService = pokemonService;
        }

        //Override Load
        public override async Task LoadAsync(NavigationParameters navigationData)
        {
            try
            {
                IsBusy = true;

                var pokemons = await _PokemonService.GetPokemonsAsync();

                foreach (var pokemon in pokemons)
                {
                    pokemon.Image = ImageHelpers.GetImageStreamFromUrl(pokemon.Sprites.FrontDefault.AbsoluteUri);

                    pokemon.ImageBack = ImageHelpers.GetImageStreamFromUrl(pokemon.Sprites.BackDefault.AbsoluteUri);

                    pokemon.AllTypes = String.Join(",", pokemon.Types.Select(p => p.Type.Name));

                    Pokemons.Add(pokemon);
                }
            }
            finally
            {
                IsBusy = false;

            }
        }

        //Using ItemTappedCommand to easy touch Listview and CollectionView Options
        private async Task ItemTappedCommandExecute(Pokemon pokemon)
        {
            //add Parameters navigation
            var parameters = new NavigationParameters();
            parameters.Add("pokemon", pokemon);

            await NavigationService.NavigateToAsync<PokemonViewModel>(parameters);
        }
    }
}
