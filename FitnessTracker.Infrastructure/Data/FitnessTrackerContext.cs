using FitnessTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructure.Data
{
    public class FitnessTrackerContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Goal> Goals { get; set; }

        public FitnessTrackerContext(DbContextOptions<FitnessTrackerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
