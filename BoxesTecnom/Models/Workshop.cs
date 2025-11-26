using System.Text.Json.Serialization;

namespace BoxesTecnom.Models
{
    public class Workshop
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        [JsonPropertyName("formatted_address")]
        public required string Address { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Whatsapp { get; set; }
        [JsonPropertyName("active")]
        public bool IsActive { get; set; }
    }
}
