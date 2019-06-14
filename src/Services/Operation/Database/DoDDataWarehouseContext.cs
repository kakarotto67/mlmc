using Microsoft.EntityFrameworkCore;
using Operation.Models;

namespace Operation.Database
{
    public class DoDDataWarehouseContext : DbContext
    {
        public DbSet<Missile> Missiles { get; set; }

        public DoDDataWarehouseContext(DbContextOptions<DoDDataWarehouseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Missile>().HasOne<MissileStatus>(x => x.Status)
            // .WithMany(x => x.Missiles)
        }
    }
}