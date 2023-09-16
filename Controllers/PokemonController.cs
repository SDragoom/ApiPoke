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
            // Inje��o de depend�ncia de HttpClient
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
                // Lida com exce��es de solicita��o HTTP, por exemplo, falha na conex�o
                return StatusCode(500, "Erro na solicita��o HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Lida com exce��es de desserializa��o JSON
                return BadRequest("O JSON n�o p�de ser desserializado: " + ex.Message);
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
                // Lida com exce��es de solicita��o HTTP, por exemplo, falha na conex�o
                return StatusCode(500, "Erro na solicita��o HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Lida com exce��es de desserializa��o JSON
                return BadRequest("O JSON n�o p�de ser desserializado: " + ex.Message);
            }
        }
    }
}
