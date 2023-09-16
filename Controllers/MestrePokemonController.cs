using APIPoke.Data;
using APIPoke.DTOs;
using APIPoke.Functions;
using APIPoke.Models;
using APIPoke.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Net.Http;

namespace APIPoke.Controllers
{
    [ApiController]
    [Route("api/mestrePokemon/")]
    public class MestrePokemonController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public MestrePokemonController(IHttpClientFactory httpClientFactory)
        {
            // Injeção de dependência de HttpClient
            _httpClient = httpClientFactory.CreateClient();

        }

        [HttpPost("post")]
        public IActionResult CreateMestrePokemon([FromBody] MestrePokemonDto mestrePokemonDto)
        {
            try
            {
                bool cpfIsvalid = CpfValidator.IsValid(mestrePokemonDto.CPF);
                string cpf = CpfValidator.CpfClean(mestrePokemonDto.CPF);
                if (cpfIsvalid)
                {

                    var mestrePokemon = new MestrePokemon
                    {
                        Nome = mestrePokemonDto.Nome,
                        Idade = mestrePokemonDto.Idade,
                        CPF = cpf,
                        PokemonsCapturados = null
                    };

                    using (var db = new MestrePokemonDbContext())
                    {
                        db.MestresPokemon.Add(mestrePokemon);
                        db.SaveChanges();
                    }

                    return Ok("Mestre Pokémon cadastrado com sucesso.");
                }
                else
                    return BadRequest("CPF invalido");
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

        [HttpGet("get")]
        public IActionResult GetMestresPokemon(int idMestrePokemon)
        {
            try
            {
                using (var db = new MestrePokemonDbContext())
                {                    
                    var mestrePokemon = db.MestresPokemon
               .Include(mp => mp.PokemonsCapturados)
               .FirstOrDefault(mp => mp.Id == idMestrePokemon);

                    if (mestrePokemon == null)
                    {
                        return NotFound("Mestre Pokémon não encontrado.");
                    }

                    return Ok(mestrePokemon);
                }

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

        [HttpGet("getall")]
        public IActionResult GetAllMestresPokemon()
        {
            try
            {
                using (var db = new MestrePokemonDbContext())
                {
                    var mestrePokemon = db.MestresPokemon.Include(mp => mp.PokemonsCapturados).ToList();

                    return Ok(mestrePokemon);
                }

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

        [HttpPut("put")]
        public IActionResult UpdateMestrePokemon(int id, [FromBody] MestrePokemonDto mestrePokemonDto)
        {
            try
            {
                bool cpfIsvalid = CpfValidator.IsValid(mestrePokemonDto.CPF);
                string cpf = CpfValidator.CpfClean(mestrePokemonDto.CPF);
                using (var db = new MestrePokemonDbContext())
                {
                    var existingMestrePokemon = db.MestresPokemon.Find(id);                   

                    // Atualizar as propriedades do Mestre Pokémon existente com os novos dados
                    existingMestrePokemon.Nome = mestrePokemonDto.Nome;
                    existingMestrePokemon.Idade = mestrePokemonDto.Idade;
                    existingMestrePokemon.CPF = cpf;

                    db.SaveChanges();
                }

                return Ok("Mestre Pokémon atualizado com sucesso.");
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

        [HttpDelete("delete")]
        public IActionResult DeleteMestrePokemon(int id)
        {
            using (var db = new MestrePokemonDbContext())
            {
                var mestrePokemon = db.MestresPokemon.Find(id);

                if (mestrePokemon == null)
                {
                    return NotFound();
                }

                db.MestresPokemon.Remove(mestrePokemon);
                db.SaveChanges();
            }

            return Ok("Mestre Pokémon excluído com sucesso.");

        }


        [HttpPost("Capture")]
        public IActionResult CapturePokemon(int idMestrePokemon, int pokemonId)
        {
            try
            {
                using (var db = new MestrePokemonDbContext())
                {
                    var existingMestrePokemon = db.MestresPokemon
               .Include(mp => mp.PokemonsCapturados)
               .FirstOrDefault(mp => mp.Id == idMestrePokemon);
                    

                    if (existingMestrePokemon == null)
                    {
                        return NotFound("Mestre Pokémon não encontrado.");
                    }

                    if (db.BoxPokemons.Any(bp => bp.MestrePokemonId == idMestrePokemon && bp.PokemonId == pokemonId))
                    {
                        return BadRequest("Este Pokémon já foi capturado pelo mestre Pokémon.");
                    }

                    var novaCaptura = new BoxPokemon
                    {
                        PokemonId = pokemonId,
                        MestrePokemonId = idMestrePokemon
                    };

                    existingMestrePokemon.PokemonsCapturados.Add(novaCaptura);

                    db.BoxPokemons.Add(novaCaptura); 
                    db.SaveChanges();
                }

                return Ok("Pokémon capturado com sucesso.");
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




        [HttpGet("GetCapturedPokemon")]
        public async Task<IActionResult> GetCapturedPokemon(int idMestre)
        {
            try
            {
                var db = new MestrePokemonDbContext();
                var pokemonsCapturados = db.MestresPokemon
               .Include(mp => mp.PokemonsCapturados)
               .FirstOrDefault(mp => mp.Id == idMestre);

                var pokemonService = new PokemonService(_httpClient);
                List<PokemonResponse> pokemonConcatenatedResults = new List<PokemonResponse>();
                var CapturedPokemon = pokemonsCapturados.PokemonsCapturados;
                
                foreach (BoxPokemon pokemon in CapturedPokemon)
                {

                    int pokemonId = pokemon.PokemonId;
                    var pokemons = await pokemonService.GetPokemonById(pokemonId);
                    pokemonConcatenatedResults.Add(pokemons);
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
