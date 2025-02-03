using Microsoft.AspNetCore.Mvc;
using HolidayRequestApi.Models;
using HolidayRequestApi.Data;

namespace HolidayRequestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HolidayRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaveRequest([FromBody] HolidayRequest dto)
        {

            _context.HolidayRequests.Add(dto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Wniosek został pomyślnie zapisany.", id = dto.Id });
        }
    }
}
