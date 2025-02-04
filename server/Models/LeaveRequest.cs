using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace LeaveRequestApi.Models
{
    //Enum z Converterem do obsługi typów wniosków przesyłanych zapytaniem
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaveType
    {
        CircumstantialHoliday,
        VacationLeave,
        OnDemandLeave,
        ChildCareLeaveDays,
        ChildCareLeaveHours
    }
    //Elementy wymagane i nie wymagane wniosku
    public class LeaveRequest
    {
        public int Id { get; set; }

        [Required]
        public LeaveType LeaveType { get; set; }

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
