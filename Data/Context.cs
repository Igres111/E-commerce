using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
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
        }
}
}
