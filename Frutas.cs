using System.Text.Json.Serialization;
namespace EspacioFrutas
{
    public class Nutritions
    {
        [JsonPropertyName("calories")]
        public int calories { get; set; }

        [JsonPropertyName("fat")]
        public double fat { get; set; }

        [JsonPropertyName("sugar")]
        public double sugar { get; set; }

        [JsonPropertyName("carbohydrates")]
        public double carbohydrates { get; set; }

        [JsonPropertyName("protein")]
        public double protein { get; set; }
    }

    public class Fruta
    {
        [JsonPropertyName("name")]
        public string? name { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("family")]
        public string? family { get; set; }

        [JsonPropertyName("order")]
        public string? order { get; set; }

        [JsonPropertyName("genus")]
        public string? genus { get; set; }

        [JsonPropertyName("nutritions")]
        public Nutritions? nutritions { get; set; }
    }


}