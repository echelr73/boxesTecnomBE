namespace BoxesTecnom.Dtos
{
    public class AppointmentResponseDto
    {
        public required int Place_id { get; set; }
        public required DateTime Appointment_at { get; set; }
        public required string Service_type { get; set; }
        public required ContactDto Contact { get; set; }
        public VehicleDto? Vehicle { get; set; }
    }
}
