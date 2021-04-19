using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVVMBase.Sample.Models;

namespace MVVMBase.Sample.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetPokemonsAsync();
    }
}
