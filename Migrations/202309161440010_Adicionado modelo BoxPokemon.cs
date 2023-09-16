namespace APIPoke.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadomodeloBoxPokemon : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoxPokemons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PokemonId = c.Int(nullable: false),
                        MestrePokemonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MestrePokemons", t => t.MestrePokemonId, cascadeDelete: true)
                .Index(t => t.MestrePokemonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoxPokemons", "MestrePokemonId", "dbo.MestrePokemons");
            DropIndex("dbo.BoxPokemons", new[] { "MestrePokemonId" });
            DropTable("dbo.BoxPokemons");
        }
    }
}
