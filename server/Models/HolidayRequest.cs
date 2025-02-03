using System.ComponentModel.DataAnnotations;

namespace HolidayRequestApi.Models
{
    public enum HolidayType
    {
        UrlopOkolicznosciowy,
        UrlopWypoczynkowy,
        UrlopNaZadanie,
        OpiekaNadDzieckiemDni,
        OpiekaNadDzieckiemGodziny
    }

    public class HolidayRequest
    {
        public int Id { get; set; }

        [Required]
        public HolidayType HolidayType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? OccasionalType { get; set; }
        public string? PersonName { get; set; }

        public string? SapNumber { get; set; }

        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public string? Comment { get; set; }
    }
}
