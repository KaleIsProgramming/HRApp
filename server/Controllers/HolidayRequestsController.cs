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
            //Sprawdzanie czy posiada token autoryzujący
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || authHeader != "Bearer hello_backend")
            {
                return Unauthorized(new { message = "Nieautoryzowany dostęp." });
            }

            // Walidacja godziny
            TimeSpan? parsedStartTime = null;
            TimeSpan? parsedEndTime = null;

            if (!string.IsNullOrWhiteSpace(dto.StartTime))
            {
                if (!TimeSpan.TryParseExact(dto.StartTime, @"hh\:mm", null, out var startTime))
                {
                    return BadRequest(new { message = "Nieprawidłowy format godziny rozpoczęcia. Użyj formatu HH:mm (np. 08:30)." });
                }
                parsedStartTime = startTime;
            }

            if (!string.IsNullOrWhiteSpace(dto.EndTime))
            {
                if (!TimeSpan.TryParseExact(dto.EndTime, @"hh\:mm", null, out var endTime))
                {
                    return BadRequest(new { message = "Nieprawidłowy format godziny zakończenia. Użyj formatu HH:mm (np. 16:45)." });
                }
                parsedEndTime = endTime;
            }
            //Walidacja daty
            if (dto.StartDate > dto.EndDate)
            {
                return BadRequest(new { message = "Data rozpoczęcia nie może być późniejsza niż data zakończenia." });
            }

            // Obsługa różnych typów wniosków
            switch (dto.HolidayType)
            {
                case HolidayType.CircumstantialHoliday:
                    if (string.IsNullOrWhiteSpace(dto.OccasionalType))
                        return BadRequest(new { message = "Należy podać typ urlopu okolicznościowego." });
                    if (string.IsNullOrWhiteSpace(dto.PersonName))
                        return BadRequest(new { message = "Należy podać typ urlopu okolicznościowego." });
                    break;
                case HolidayType.OnDemandHoliday:
                    if (string.IsNullOrWhiteSpace(dto.SapNumber))
                        return BadRequest(new { message = "Numer SAP jest wymagany w przypadku urlopu na żądanie." });
  
                    if (!Regex.IsMatch(dto.SapNumber, @"^\d{8}$"))
                        return BadRequest(new { message = "Numer SAP musi być ośmio-cyfrowym numerem." });
                    break;
                case HolidayType.ChildCareLeaveHours:
                    if (!parsedStartTime.HasValue || !parsedEndTime.HasValue)
                        return BadRequest(new { message = "Należy podać godzinę rozpoczęcia i zakończenia." });
                    if (parsedStartTime >= parsedEndTime)
                        return BadRequest(new { message = "Godzina rozpoczęcia musi być wcześniejsza niż godzina zakończenia." });
                    break;

            }

            //Zapis za pomocą entityframework
            _context.HolidayRequests.Add(dto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Wniosek został pomyślnie zapisany.", id = dto.Id });
        }
    }
}
