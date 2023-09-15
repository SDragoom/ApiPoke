using System;
using System.Net.Http;
using System.Threading.Tasks;
using APIPoke.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIPoke.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonListController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PokemonListController(IHttpClientFactory httpClientFactory)
        {
            // Inje��o de depend�ncia de HttpClient
            _httpClient = httpClientFactory.CreateClient();
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPokemon()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");
                response.EnsureSuccessStatusCode(); // Lan�a uma exce��o em caso de erro HTTP

                var content = await response.Content.ReadAsStringAsync();
                var pokemonList = JsonConvert.DeserializeObject<PokemonList>(content);
                return Ok(pokemonList);
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
