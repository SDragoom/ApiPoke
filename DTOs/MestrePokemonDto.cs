using APIPoke.Models;

namespace APIPoke.DTOs
{
    public class MestrePokemonDto
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }

        public List<BoxPokemon> PokemonsCapturados { get; set; }
        
        public MestrePokemonDto()
        {

            PokemonsCapturados = new List<BoxPokemon>();
        }
    }
}
