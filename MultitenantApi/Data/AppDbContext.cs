using Microsoft.EntityFrameworkCore;

namespace MultitenantApi.Data
{
    public class AppDbContext   : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(@"Data Source=.\Data\main.db;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Item> Items { get; set; }
    }
}