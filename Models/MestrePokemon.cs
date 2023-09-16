namespace APIPoke.Models
{
    public class MestrePokemon
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }

        public List<BoxPokemon?>? PokemonsCapturados { get; set; }

    }
}
