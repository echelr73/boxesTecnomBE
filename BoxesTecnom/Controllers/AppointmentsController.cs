using BoxesTecnom.Dtos;
using BoxesTecnom.Models;
using BoxesTecnom.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoxesTecnom.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsService _appointmentsService;
        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Appointment>> GetAppointments()
        {
            var appointments = _appointmentsService.GetAppointments();
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<ActionResult> AddAppointment([FromBody] AppointmentRequestDto appointmentDto)
        {
            var result = await _appointmentsService.AddAppointment(appointmentDto);
            if (!result.ok)
            {
                return BadRequest(new { error = result.error });
            }
            return Ok(appointmentDto);
        }
    }
}
