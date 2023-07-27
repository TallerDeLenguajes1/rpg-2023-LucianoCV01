using System.Text.Json.Serialization;
namespace EspacioCitas
{
    public class Cita
    {
        [JsonPropertyName("_id")]
        public string? _id { get; set; }

        [JsonPropertyName("content")]
        public string? content { get; set; }

        [JsonPropertyName("author")]
        public string? author { get; set; }

        [JsonPropertyName("tags")]
        public List<string>? tags { get; set; }

        [JsonPropertyName("authorSlug")]
        public string? authorSlug { get; set; }

        [JsonPropertyName("length")]
        public int length { get; set; }

        [JsonPropertyName("dateAdded")]
        public string? dateAdded { get; set; }

        [JsonPropertyName("dateModified")]
        public string? dateModified { get; set; }
    }
}