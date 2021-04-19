using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MVVMBase.Sample.Helpers;
using MVVMBase.Sample.Models;

namespace MVVMBase.Sample.Services
{
    public class PokemonService : IPokemonService
    {
        public async Task<List<Pokemon>> GetPokemonsAsync()
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            try
            {
                var httpClient = new HttpClient();

                for (int i = 1; i < 20; i++)
                {
                    var response = await httpClient.
                        GetAsync<Pokemon>($"{Constants.ApiURL}/pokemon/{i}");

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        pokemons.Add(response.Value);
                    }
                    else
                    {
                        Debug.WriteLine(response.Error.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return pokemons;


        }
    }
}
