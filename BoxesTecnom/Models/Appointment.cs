namespace BoxesTecnom.Models
{
    public class Appointment
    {
        public required int Place_Id { get; set; }
        public required DateTime Appointment_At { get; set; }
        public required string Service_Type { get; set; }
        public required Contact Contact { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
