using FlightsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightsAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Leg> Legs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var leg1 = new Leg { Id = 1, CrossingTime = 1000, nextLegId = 2 };
            var leg2 = new Leg { Id = 2, CrossingTime = 1000, nextLegId = 3 };
            var leg3 = new Leg { Id = 3, CrossingTime = 2000, nextLegId = 4 };
            var leg4 = new Leg { Id = 4, CrossingTime = 8000, nextLegId = 5 };
            var leg5 = new Leg { Id = 5, CrossingTime = 3000, nextLegId = 6 };
            var leg6 = new Leg { Id = 6, CrossingTime = 5000, nextLegId = 7 };
            var leg7 = new Leg { Id = 7, CrossingTime = 5000, nextLegId = 8 };
            var leg8 = new Leg { Id = 8, CrossingTime = 2000, nextLegId = 4 };
            modelBuilder.Entity<Leg>().HasData(leg1,leg2,leg3,leg4,leg5,leg6,leg7,leg8);
        }
    }
}
