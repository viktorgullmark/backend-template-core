using Microsoft.EntityFrameworkCore;

namespace TimeTrackerBackend.Models
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        { }

        public DbSet<User> tblUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tblUser

            modelBuilder.Entity<User>()
                .Property(b => b.Guid)
                .HasDefaultValueSql("newid()");
            modelBuilder.Entity<User>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>()
                .Property(b => b.Modified)
                .HasDefaultValueSql("getdate()");

            #endregion
        }
    }
}
