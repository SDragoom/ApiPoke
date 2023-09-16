using APIPoke.Data;
using APIPoke.DTOs;
using APIPoke.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIPoke.Controllers
{
    [ApiController]
    [Route("api/mestrePokemon/")]
    public class MestrePokemonController : ControllerBase
    {    

        [HttpPost("post")]
        public async Task<IActionResult> CreateMestrePokemon([FromBody] MestrePokemonDto mestrePokemonDto)
        {
            try
            {
                var mestrePokemon = new MestrePokemon
                {
                    Nome = mestrePokemonDto.Nome,
                    Idade = mestrePokemonDto.Idade,
                    CPF = mestrePokemonDto.CPF
                };

                using (var db = new MestrePokemonDbContext())
                {
                    db.MestresPokemon.Add(mestrePokemon);
                    db.SaveChanges();
                }

                return Ok("Mestre Pok�mon cadastrado com sucesso." + mestrePokemonDto.ToString());
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

        [HttpGet("get")]
        public async Task<IActionResult> GetMestresPokemon(int id)
        {
            try
            {
                using (var db = new MestrePokemonDbContext())
                {
                    var mestrePokemon = db.MestresPokemon.Find(id);
                                       
                    return Ok(mestrePokemon);
                }

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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllMestresPokemon()
        {
            try
            {
                using (var db = new MestrePokemonDbContext())
                {
                    var mestrePokemon = db.MestresPokemon.ToList();

                    return Ok(mestrePokemon);
                }

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

        [HttpPut("put")]
        public async Task<IActionResult> UpdateMestrePokemon(int id, [FromBody] MestrePokemonDto mestrePokemonDto)
        {
            try
            {
                using (var db = new MestrePokemonDbContext())
                {
                    var existingMestrePokemon = db.MestresPokemon.Find(id);

                    if (existingMestrePokemon == null)
                    {
                        return NotFound();
                    }

                    // Atualizar as propriedades do Mestre Pok�mon existente com os novos dados
                    existingMestrePokemon.Nome = mestrePokemonDto.Nome;
                    existingMestrePokemon.Idade = mestrePokemonDto.Idade;
                    existingMestrePokemon.CPF = mestrePokemonDto.CPF;

                    db.SaveChanges();
                }

                return Ok("Mestre Pok�mon atualizado com sucesso." + mestrePokemonDto.ToString());
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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMestrePokemon(int id)
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

            return Ok("Mestre Pok�mon exclu�do com sucesso.");

        }

    }
}
