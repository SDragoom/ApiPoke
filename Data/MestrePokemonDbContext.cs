using APIPoke.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;


namespace APIPoke.Data
{
    public class MestrePokemonDbContext : DbContext
    {
        public DbSet<MestrePokemon> MestresPokemon { get; set; }

        public DbSet<BoxPokemon> BoxPokemons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuração da relação entre MestrePokemon e BoxPokemon
            modelBuilder.Entity<MestrePokemon>()
                .HasMany(m => m.PokemonsCapturados)
                .WithRequired()
                .HasForeignKey(bp => bp.MestrePokemonId);
        }



        public MestrePokemonDbContext(DbConnectionStringBuilder dbConnectionStringBuilder) : base("Data Source=DataDirectoryPokemon.db")
        {

        }

        public MestrePokemonDbContext()
        {
        }
        
    }
}
