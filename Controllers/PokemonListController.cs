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
            // Injeção de dependência de HttpClient
            _httpClient = httpClientFactory.CreateClient();
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPokemon()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon/");
                response.EnsureSuccessStatusCode(); // Lança uma exceção em caso de erro HTTP

                var content = await response.Content.ReadAsStringAsync();
                var pokemonList = JsonConvert.DeserializeObject<PokemonList>(content);
                return Ok(pokemonList);
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
