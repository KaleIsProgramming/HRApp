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
                return BadRequest(new { message = "Data początkowa nie może być późniejsza niż data końcowa." });
            }


            switch (dto.HolidayType)
            {
                case HolidayType.UrlopOkolicznosciowy:
                    if (string.IsNullOrWhiteSpace(dto.OccasionalType))
                        return BadRequest(new { message = "Należy podać rodzaj urlopu okolicznościowego." });
                    if (string.IsNullOrWhiteSpace(dto.PersonName))
                        return BadRequest(new { message = "Należy podać imię i nazwisko osoby, której dotyczy wniosek." });
                    break;
                case HolidayType.UrlopNaZadanie:
                    if (string.IsNullOrWhiteSpace(dto.SapNumber))
                        return BadRequest(new { message = "Numer SAP jest wymagany dla urlopu na żądanie." });
                    break;
                case HolidayType.OpiekaNadDzieckiemGodziny:
                    if (!dto.StartTime.HasValue || !dto.EndTime.HasValue)
                        return BadRequest(new { message = "Należy podać godzinę początkową i końcową." });
                    if (dto.StartTime >= dto.EndTime)
                        return BadRequest(new { message = "Godzina początkowa musi być wcześniejsza niż godzina końcowa." });
                    break;

            }
            _context.HolidayRequests.Add(dto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Wniosek został pomyślnie zapisany.", id = dto.Id });
        }
    }
}
