using System.Text.Json.Serialization;

namespace City_maps.Models
{
    public class City
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("district")]
        public string District { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("population")]
        public int Population { get; set; }
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        
        [JsonPropertyName("coords")]
        public Coords Coords { get; set; }
    }
}