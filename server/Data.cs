using Microsoft.EntityFrameworkCore;
using HolidayRequestApi.Models;

namespace HolidayRequestApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<HolidayRequest> HolidayRequests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}