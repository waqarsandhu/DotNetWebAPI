using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Store.Models
{
    public class CommerceDbContext : DbContext
    {
        public CommerceDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Name> Names { get; set; }
        public DbSet<Geolocation> Geolocations { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Name)
                .WithOne(n => n.User)
                .HasForeignKey<User>(u => u.NameId);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Geolocation)
                .WithOne(g => g.Address)
                .HasForeignKey<Address>(a => a.GeolocationId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
