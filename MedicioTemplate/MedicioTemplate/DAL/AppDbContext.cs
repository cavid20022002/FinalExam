using MedicioTemplate.Models;
using MedicioTemplate.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MedicioTemplate.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }

        public DbSet<CustomService> CustomServices { get; set; }
        public DbSet<Testimonial> Testimonials { get; set;}
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Setting> Settings { get; set; }


    }

}
