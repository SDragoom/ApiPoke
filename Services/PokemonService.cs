using APIPoke.DTOs;
using APIPoke.Functions;
using APIPoke.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIPoke.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PokemonResponse> GetPokemonById(int id)
        {
            var pokemonService = new PokemonService(_httpClient); // Supondo que você tenha uma instância de HttpClient (_httpClient) disponível
            var urlevolution = await pokemonService.GetEvolutionChainUrlAsync(id);
            var pokemon = await pokemonService.GetPokemonByIdAsync(id);
            var evolution = await pokemonService.GetEvolutionAsync(urlevolution);
            var evolutionurlList = ExtractIdFromUrlFunction.ExtractEvolutionUrls(evolution.Chain);
            var evolutionIds = ExtractIdFromUrlFunction.ExtractIdFromUrls(evolutionurlList);

            List<PokemonEvolution> pokemonEvolutionConcatenatedResults = new List<PokemonEvolution>();

            if (evolutionIds != null)
            {
                foreach (var evolutionid in evolutionIds)
                {
                    var pokemonevolution = await pokemonService.GetPokemonByIdEvolution(evolutionid);
                    pokemonEvolutionConcatenatedResults.Add(pokemonevolution);
                }
            }
            PokemonResponse pokemonresponse = new PokemonResponse()
            {
                Name = pokemon.Name,
                Id = pokemon.Id,
                Abilities = pokemon.Abilities,
                Sprites = pokemon.Sprites,
                Stats = pokemon.Stats,
                Types = pokemon.Types,
                Height = pokemon.Height,
                Weight = pokemon.Weight,
                EvolutionChain = pokemonEvolutionConcatenatedResults
            };

            return pokemonresponse;
        }
        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Pokemon>(content);
        }
        public async Task<Pokemon> GetPokemonByName(string name)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Pokemon>(content);
        }
        public async Task<PokemonEvolution> GetPokemonByIdEvolution(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PokemonEvolution>(content);
        }
        public async Task<string> GetEvolutionChainUrlAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var pokemonspecies = JsonConvert.DeserializeObject<PokemonSpecies>(content);
            string evolutionUri = pokemonspecies.EvolutionChain.Url.ToString();
            return evolutionUri;
        }
        public async Task<EvolutionChain> GetEvolutionAsync(string evolutionUri)
        {
            var response = await _httpClient.GetAsync(evolutionUri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var evolution = JsonConvert.DeserializeObject<EvolutionChain>(content);
            return evolution;
        }
        public async Task<int> GetCountPokemonAsync()
        {
            var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var pokemonList = JsonConvert.DeserializeObject<PokemonList>(content);
            return pokemonList.Count ?? 0; ;
        }
    }
}
