using MyPortfolio.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using MyPortfolio.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace MyPortfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<FormMobileSellIt> FormMobileSellIts { get; set; }
        public virtual DbSet<Comment>Comments { get; set; }
        public virtual DbSet<StripeModel> Stripes { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Likes> Likess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data
            modelBuilder.Entity<Brand>().HasData(
               new Brand { Id = 1, brand = "Samsung", MobileModel = "Galaxy S20", UsedYears = "1" },
        new Brand { Id = 2, brand = "Apple", MobileModel = "iPhone 12", UsedYears = "0.5" },
        new Brand { Id = 3, brand = "Sony", MobileModel = "Xperia 5", UsedYears = "2" },
        new Brand { Id = 4, brand = "Google", MobileModel = "Pixel 5", UsedYears = "0.5" },
        new Brand { Id = 5, brand = "OnePlus", MobileModel = "8T", UsedYears = "1.5" },
        new Brand { Id = 6, brand = "Xiaomi", MobileModel = "Mi 10T Pro", UsedYears = "0.8" },
        new Brand { Id = 7, brand = "Huawei", MobileModel = "P40 Pro", UsedYears = "1.2" },
        new Brand { Id = 8, brand = "LG", MobileModel = "Velvet", UsedYears = "2.5" },
        new Brand { Id = 9, brand = "Motorola", MobileModel = "Moto G Power", UsedYears = "0.3" },
        new Brand { Id = 10, brand = "Nokia", MobileModel = "8.3 5G", UsedYears = "1.8" },
        new Brand { Id = 11, brand = "Asus", MobileModel = "Zenfone 7 Pro", UsedYears = "0.7" },
        new Brand { Id = 12, brand = "BlackBerry", MobileModel = "KEY2", UsedYears = "2.3" },
        new Brand { Id = 13, brand = "HTC", MobileModel = "U12 Plus", UsedYears = "3.0" },
        new Brand { Id = 14, brand = "Oppo", MobileModel = "Find X2 Pro", UsedYears = "0.9" },
        new Brand { Id = 15, brand = "Realme", MobileModel = "X7 Pro", UsedYears = "0.6" },
        new Brand { Id = 16, brand = "Vivo", MobileModel = "X50 Pro", UsedYears = "1.7" },
        new Brand { Id = 17, brand = "Lenovo", MobileModel = "Legion Phone Duel", UsedYears = "0.4" },
        new Brand { Id = 18, brand = "ZTE", MobileModel = "Axon 20 5G", UsedYears = "1.4" },
        new Brand { Id = 19, brand = "Poco", MobileModel = "X3 NFC", UsedYears = "0.2" },
        new Brand { Id = 20, brand = "TCL", MobileModel = "10 Pro", UsedYears = "2.1" }
            );

        }
    }
}
