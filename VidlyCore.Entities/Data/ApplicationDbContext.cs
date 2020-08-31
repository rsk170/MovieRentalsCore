using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VidlyCore.Entities.Models;

namespace VidlyCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MembershipType>().HasData(
                new MembershipType { Id = 1, SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0, Name = "Pay as you go" },
                new MembershipType { Id = 2, SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10, Name = "Monthly" },
                new MembershipType { Id = 3, SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15, Name = "Quarterly" },
                new MembershipType { Id = 4, SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20, Name = "Annual" }
                );

            builder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action"},
                new Genre { Id = 2, Name = "Thriller"},
                new Genre { Id = 3, Name = "Family"},
                new Genre { Id = 4, Name = "Romance"},
                new Genre { Id = 5, Name = "Comedy"}
                );
        }
    }
}
