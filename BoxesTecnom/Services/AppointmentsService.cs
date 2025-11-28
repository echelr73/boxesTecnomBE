using BoxesTecnom.Dtos;
using BoxesTecnom.Models;

namespace BoxesTecnom.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly IWorkshopsService _workshopsService;
        private static readonly List<Appointment> _appointments = new List<Appointment>();
        public AppointmentsService(IWorkshopsService workshopsService)
        {
            _workshopsService = workshopsService;
        }
        public IEnumerable<AppointmentResponseDto> GetAppointments()
        {
            return _appointments.Select(a => new AppointmentResponseDto
            {
                Place_id = a.Place_id,
                Appointment_at = a.Appointment_at,
                Service_type = a.Service_type,
                Contact = new ContactDto
                {
                    Name = a.Contact.Name,
                    Email = a.Contact.Email,
                    Phone = a.Contact.Phone
                },
                Vehicle = a.Vehicle != null ? new VehicleDto
                {
                    Make = a.Vehicle.Make,
                    Model = a.Vehicle.Model,
                    Year = a.Vehicle.Year,
                    License_Plate = a.Vehicle.License_plate
                } : null
            });
        }
        public async Task<(bool ok, List<string>? error)> AddAppointment(AppointmentRequestDto appointmentDto)
        {
            var listErrors = ValidateAppointment(appointmentDto);

            var activeWorkshops = await _workshopsService.GetWorkshopsAsync();
            if (!activeWorkshops.Any(w => w.Id == appointmentDto.Place_id))
            {
                listErrors.Add("Invalid or inactive place_id: " + appointmentDto.Place_id);
            }
            if (listErrors.Count() > 0)
                return (false, listErrors);

            var appointment = new Appointment
            {
                Place_id = appointmentDto.Place_id,
                Appointment_at = appointmentDto.Appointment_at,
                Service_type = appointmentDto.Service_type,
                Contact = new Contact
                {
                    Name = appointmentDto.Contact.Name,
                    Email = appointmentDto.Contact.Email,
                    Phone = appointmentDto.Contact.Phone ?? null
                },
                Vehicle = appointmentDto.Vehicle != null ? new Vehicle
                {
                    Make = appointmentDto.Vehicle.Make,
                    Model = appointmentDto.Vehicle.Model,
                    Year = appointmentDto.Vehicle.Year,
                    License_plate = appointmentDto.Vehicle.License_Plate
                } : null
            };
            _appointments.Add(appointment);
            return (true, null);
        }

        private List<string> ValidateAppointment(AppointmentRequestDto aDto)
        {
            var errors = new List<string>();

            if (aDto.Contact == null)
                errors.Add("The 'contact' object is required.");

            if (string.IsNullOrWhiteSpace(aDto.Contact.Name))
                errors.Add("The field 'contact.name' is required.");

            if (string.IsNullOrWhiteSpace(aDto.Contact.Email))
                errors.Add("The field 'contact.email' is required.");

            if (string.IsNullOrWhiteSpace(aDto.Service_type))
                errors.Add("The field 'service_type' is required.");

            if (aDto.Place_id <= 0)
                errors.Add("The field 'place_id' must be a positive integer.");

            if (aDto.Appointment_at == default)
                errors.Add("The field 'appointment_at' is required.");

            return errors;
        }
    }
}
