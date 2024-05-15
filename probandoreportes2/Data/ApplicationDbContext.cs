using Microsoft.EntityFrameworkCore;
using probandoreportes2.Models;

namespace probandoreportes2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
    }
}
