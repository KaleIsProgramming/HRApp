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

            if (dto.StartDate > dto.EndDate)
            {
                return BadRequest(new { message = "The start date cannot be later than the end date." });
            }


            switch (dto.HolidayType)
            {
                case HolidayType.CircumstantialHoliday:
                    if (string.IsNullOrWhiteSpace(dto.OccasionalType))
                        return BadRequest(new { message = "The type of circumstantial leave must be provided." });
                    if (string.IsNullOrWhiteSpace(dto.PersonName))
                        return BadRequest(new { message = "The full name of the person concerned by the request must be provided" });
                    break;
                case HolidayType.OnDemandHoliday:
                    if (string.IsNullOrWhiteSpace(dto.SapNumber))
                        return BadRequest(new { message = "The SAP number is required for on-demand leave." });
                    break;
                case HolidayType.ChildCareLeaveDays:
                    if (!dto.StartTime.HasValue || !dto.EndTime.HasValue)
                        return BadRequest(new { message = "The start and end time must be provided." });
                    if (dto.StartTime >= dto.EndTime)
                        return BadRequest(new { message = "The start time must be earlier than the end time." });
                    break;

            }
            _context.HolidayRequests.Add(dto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "The request has been successfully saved.", id = dto.Id });
        }
    }
}
