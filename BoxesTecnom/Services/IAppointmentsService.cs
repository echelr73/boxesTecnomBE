using BoxesTecnom.Dtos;
using BoxesTecnom.Models;

namespace BoxesTecnom.Services
{
    public interface IAppointmentsService
    {
        IEnumerable<AppointmentResponseDto> GetAppointments();
        Task<(bool ok, List<string>? error)> AddAppointment(AppointmentRequestDto appointment);
    }
}
