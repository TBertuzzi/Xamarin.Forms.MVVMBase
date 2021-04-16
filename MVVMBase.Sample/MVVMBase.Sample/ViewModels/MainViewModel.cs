using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MVVMBase.Sample.Helpers;
using MVVMBase.Sample.Models;
using MVVMBase.Sample.Services;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Xamarin.Forms.MVVMBase.ViewModels;

namespace MVVMBase.Sample.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Pokemon> Pokemons { get; }
        IPokemonService _PokemonService;

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
    }
}
