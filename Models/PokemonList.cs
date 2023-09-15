using System.Text.Json.Serialization;

namespace APIPoke.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Result
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class PokemonList
    {
        [JsonPropertyName("count")]
        public int? Count { get; set; }

        [JsonPropertyName("next")]
        public string? Next { get; set; }

        [JsonPropertyName("previous")]
        public object? Previous { get; set; }

        [JsonPropertyName("results")]
        public List<Result>? Results { get; set; }
    }


}
