using Microsoft.AspNetCore.Mvc;
using HolidayRequestApi.Models;
using HolidayRequestApi.Data;
using System.Text.RegularExpressions;

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
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || authHeader != "Bearer hello_backend")
            {
                return Unauthorized(new { message = "Nieautoryzowany dostęp." });
            }

            TimeSpan? parsedStartTime = null;
            TimeSpan? parsedEndTime = null;

            if (!string.IsNullOrWhiteSpace(dto.StartTime))
            {
                if (!TimeSpan.TryParseExact(dto.StartTime, @"hh\:mm", null, out var startTime))
                {
                    return BadRequest(new { message = "Invalid StartTime format. Use HH:mm (e.g., 08:30)." });
                }
                parsedStartTime = startTime;
            }

            if (!string.IsNullOrWhiteSpace(dto.EndTime))
            {
                if (!TimeSpan.TryParseExact(dto.EndTime, @"hh\:mm", null, out var endTime))
                {
                    return BadRequest(new { message = "Invalid EndTime format. Use HH:mm (e.g., 16:45)." });
                }
                parsedEndTime = endTime;
            }

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
  
                    if (!Regex.IsMatch(dto.SapNumber, @"^\d{8}$"))
                        return BadRequest(new { message = "Numer SAP musi być ośmio-cyfrowym numerem." });
                    break;
                case HolidayType.ChildCareLeaveHours:
                    if (!parsedStartTime.HasValue || !parsedEndTime.HasValue)
                        return BadRequest(new { message = "The start and end time must be provided." });
                    if (parsedStartTime >= parsedEndTime)
                        return BadRequest(new { message = "The start time must be earlier than the end time." });
                    break;

            }
            _context.HolidayRequests.Add(dto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "The request has been successfully saved.", id = dto.Id });
        }
    }
}
