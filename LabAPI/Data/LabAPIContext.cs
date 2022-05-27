using Microsoft.EntityFrameworkCore;

namespace LabAPI.Models
{
    public class LabAPIContext : DbContext
    {
        public LabAPIContext(DbContextOptions option) : base(option)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
    }
}
