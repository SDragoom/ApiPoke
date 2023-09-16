namespace APIPoke.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PokemonEvolution
    {
        //[JsonProperty("abilities")]
        //public Ability[]? Abilities { get; set; }

        //[JsonProperty("base_experience")]
        //public long BaseExperience { get; set; }

        //[JsonProperty("forms")]
        //public Species[]? Forms { get; set; }

        //[JsonProperty("game_indices")]
        //public GameIndex[]? GameIndices { get; set; }

        //[JsonProperty("height")]
        //public long Height { get; set; }

        //[JsonProperty("held_items")]
        //public object[]? HeldItems { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        //[JsonProperty("is_default")]
        //public bool IsDefault { get; set; }

        //[JsonProperty("location_area_encounters")]
        //public Uri? LocationAreaEncounters { get; set; }

        //[JsonProperty("moves")]
        //public Move[]? Moves { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        //[JsonProperty("order")]
        //public long Order { get; set; }

        //[JsonProperty("past_types")]
        //public object[]? PastTypes { get; set; }

        //[JsonProperty("species")]
        //public Species? Species { get; set; }

        [JsonProperty("sprites")]
        public Sprites? Sprites { get; set; }

        //[JsonProperty("stats")]
        //public Stat[]? Stats { get; set; }

        [JsonProperty("types")]
        public TypeElement[]? Types { get; set; }

        //[JsonProperty("weight")]
        //public long Weight { get; set; }
    }

   
}
