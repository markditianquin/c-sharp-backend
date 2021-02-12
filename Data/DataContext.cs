using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
        .Property(user => user.DateCreated).HasDefaultValueSql("getutcdate()");
    }
  }
}