namespace BoxesTecnom.Models
{
    public class Appointment
    {
        public required int Place_id { get; set; }
        public required DateTime Appointment_at { get; set; }
        public required string Service_type { get; set; }
        public required Contact Contact { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
