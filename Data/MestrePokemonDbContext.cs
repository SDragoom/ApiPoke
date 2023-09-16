using APIPoke.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;


namespace APIPoke.Data
{
    public class MestrePokemonDbContext : DbContext
    {
        public DbSet<MestrePokemon> MestresPokemon { get; set; }

        public MestrePokemonDbContext(DbConnectionStringBuilder dbConnectionStringBuilder) : base("Data Source=DataDirectoryPokemon.db")
        {

        }

        public MestrePokemonDbContext()
        {
        }
    }
}
