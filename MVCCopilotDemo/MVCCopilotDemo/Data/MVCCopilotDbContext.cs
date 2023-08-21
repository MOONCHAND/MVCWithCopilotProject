using Microsoft.EntityFrameworkCore;
using MVCCopilotDemo.Models.Domain;

namespace MVCCopilotDemo.Data
{
    public class MVCCopilotDbContext :DbContext
    {
        //generate a constructor that takes a DbContextOptions<MVCCopilotDbContext> object as a parameter
        public MVCCopilotDbContext(DbContextOptions<MVCCopilotDbContext> options) : base(options)
        {
        }

        // generate a DbSet<> of type Employee named Employees
        public DbSet<Employee> Employees { get; set; }

    }
}
