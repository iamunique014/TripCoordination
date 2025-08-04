using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripCoordination.Areas.Identity.Data;
using TripCoordination.Data.Models.Domain;

//namespace TripCoordination.Data.Models.Data;

//public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//{
//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }

//    protected override void OnModelCreating(ModelBuilder builder)
//    {
//        base.OnModelCreating(builder);

//        builder.Entity<ApplicationUser>()
//               .Property(u => u.CreatedAt)
//               .HasDefaultValueSql("GETUTCDATE()");
//    }
//}
