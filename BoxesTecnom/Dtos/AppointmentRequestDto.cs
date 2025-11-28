using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoxesTecnom.Dtos
{
    public class AppointmentRequestDto
    {
        [JsonPropertyName("place_id")]
        public required int Place_id { get; set; }

        [JsonPropertyName("appointment_at")]
        public required DateTime Appointment_at { get; set; }

        [JsonPropertyName("service_type")]
        public required string Service_type { get; set; }

        public required ContactDto Contact { get; set; }

        public VehicleDto? Vehicle { get; set; }
    }
}
