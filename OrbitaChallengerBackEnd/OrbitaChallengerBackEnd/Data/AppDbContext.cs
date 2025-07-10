using Microsoft.EntityFrameworkCore;
using OrbitaChallengerBackEnd.Models;
using System.Collections.Generic;

namespace OrbitaChallengerBackEnd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
