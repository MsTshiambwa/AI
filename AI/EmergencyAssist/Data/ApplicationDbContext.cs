using Microsoft.EntityFrameworkCore;
using EmergencyAssist.Models;

namespace EmergencyAssist.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<LocationData> Locations { get; set; }
        public DbSet<EmotionData> Emotions { get; set; }
    }
}

