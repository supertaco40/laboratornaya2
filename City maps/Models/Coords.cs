using System;
using System.Text.Json.Serialization;

namespace City_maps.Models
{
    [Serializable]
    public class Coords
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lon")]
        public string Lon { get; set; }
    }
}