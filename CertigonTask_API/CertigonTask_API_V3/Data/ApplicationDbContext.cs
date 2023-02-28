using CertigonTask_API_V3.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CertigonTask_API_V3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Item { get; set; }
        public DbSet<AuthenticationToken> AuthenticationToken{ get; set; }
        public DbSet<UserAccount> UserAccount{ get; set; }

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //ovdje pise FluentAPI konfiguraciju
        }
    }
}
