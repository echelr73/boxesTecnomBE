using BoxesTecnom.Models;
using BoxesTecnom.Services;
using Microsoft.AspNetCore.Mvc;

namespace BoxesTecnom.Controllers
{
    [ApiController]
    [Route("api/workshops")]
    public class WorkshopsController : Controller
    {
        private readonly IWorkshopsService _workshopsService;

        public WorkshopsController(IWorkshopsService workshopsService)
        {
            _workshopsService = workshopsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workshop>>> GetWorkshops()
        {
            var workshops = await _workshopsService.GetWorkshopsAsync();
            return Ok(workshops);
        }
    }
}
