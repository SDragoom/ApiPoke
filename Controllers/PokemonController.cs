using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using APIPoke.DTOs;
using APIPoke.Models;
using APIPoke.Functions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using APIPoke.Services;

namespace APIPoke.Controllers
{
    [ApiController]
    [Route("api/pokemon/")]
    public class PokemonController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PokemonController(IHttpClientFactory httpClientFactory)
        {
            // Injeção de dependência de HttpClient
            _httpClient = httpClientFactory.CreateClient();

        }

        [HttpGet("GetPokemon")]
        public async Task<IActionResult> GetPokemon(int id)
        {
            try
            {
                var pokemonService = new PokemonService(_httpClient);
                var pokemon = await pokemonService.GetPokemonById(id);
                return Ok(pokemon);
            }
            catch (HttpRequestException ex)
            {
                // Lida com exceções de solicitação HTTP, por exemplo, falha na conexão
                return StatusCode(500, "Erro na solicitação HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Lida com exceções de desserialização JSON
                return BadRequest("O JSON não pôde ser desserializado: " + ex.Message);
            }
        }
        [HttpGet("Get10RandomPokemon")]
        public async Task<IActionResult> Get10RandomPokemon()
        {
            try
            {   
                var pokemonService = new PokemonService(_httpClient);
                List<PokemonResponse> pokemonConcatenatedResults = new List<PokemonResponse>();
                var rand = new Random();
                //var count = await pokemonService.GetCountPokemonAsync();
                for (int i = 0; i < 10; i++)
                {
                    var randomId = rand.Next(1,1010);
                    var pokemon = await pokemonService.GetPokemonById(randomId);
                    pokemonConcatenatedResults.Add(pokemon);
                }
            
                
                return Ok(pokemonConcatenatedResults);
            }
            catch (HttpRequestException ex)
            {
                // Lida com exceções de solicitação HTTP, por exemplo, falha na conexão
                return StatusCode(500, "Erro na solicitação HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Lida com exceções de desserialização JSON
                return BadRequest("O JSON não pôde ser desserializado: " + ex.Message);
            }
        }
    }
}
