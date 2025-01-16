using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserForProduct> UserForProducts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<BillingInfo> BillingInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(entity => entity.Id);
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasOne(entity => entity.User)
                .WithMany(entity => entity.RefreshTokens)
                .HasForeignKey(entity => entity.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<UserForProduct>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.HasOne(entity => entity.Product)
                .WithMany(entity => entity.UserForProducts)
                .HasForeignKey(entity => entity.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(entity => entity.User)
               .WithMany(entity => entity.UserForProducts)
               .HasForeignKey(entity => entity.UserId)
               .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<BillingInfo>(entity =>
            {
                entity.HasKey(entity => entity.Id);

                entity.HasOne(entity => entity.User)
                .WithMany(entity => entity.billingInfos)
                .HasForeignKey(entity => entity.UserId) 
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
