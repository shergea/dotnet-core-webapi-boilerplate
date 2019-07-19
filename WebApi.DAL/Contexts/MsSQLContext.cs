using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.DAL
{
    public partial class MsSQLContext : DbContext
    {
        public MsSQLContext(DbContextOptions<MsSQLContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<RefreshToken>()
                .HasKey(c => c.Token);
        }
    }
}