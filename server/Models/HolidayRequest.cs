using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace HolidayRequestApi.Models
{
    //Enum z Converterem do obsługi typów wniosków przesyłanych zapytaniem
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HolidayType
    {
        CircumstantialHoliday,
        VacationLeave,
        OnDemandHoliday,
        ChildCareLeaveDays,
        ChildCareLeaveHours
    }
    //Elementy wymagane i nie wymagane wniosku
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

        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public string? Comment { get; set; }
    }
}
