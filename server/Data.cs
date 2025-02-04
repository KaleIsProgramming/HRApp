using Microsoft.EntityFrameworkCore;
using LeaveRequestApi.Models;

//Entity framework do szybkiego zapisywania

namespace LeaveRequestApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}