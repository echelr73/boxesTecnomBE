namespace BoxesTecnom.Dtos
{
    public class AppointmentResponseDto
    {
        public required int Place_Id { get; set; }
        public required DateTime Appointment_At { get; set; }
        public required string Service_Type { get; set; }
        public required ContactDto Contact { get; set; }
        public VehicleDto? Vehicle { get; set; }
    }
}
