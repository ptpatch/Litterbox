using Litterbox.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LitterBox.Data
{
    public class LitterboxContext : IdentityDbContext<LitterboxUser>
    {
        public LitterboxContext() : base("name=LitterboxConnectionString")
        {
            Database.SetInitializer<LitterboxContext>(new LitterboxDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Promo>()
                .HasIndex(p => new { p.Code })
                .IsUnique(true);

            modelBuilder.Entity<LitterboxUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }

        public static LitterboxContext Create()
        {
            return new LitterboxContext();
        }
    }
}