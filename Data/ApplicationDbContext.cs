using BookBase.Models;
using Microsoft.EntityFrameworkCore;


namespace BookBase.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd(); //Indicates auto increment

                // Setting specific properties to use varchar with a specified max length

                entity.Property(e => e.Username)
                      .HasColumnType("varchar(120)")
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasColumnType("varchar(120)")
                      .IsRequired();

                entity.Property(e => e.FirstName)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .HasColumnType("varchar(50)")
                      .IsRequired();

                entity.Property(e => e.PasswordHash)
                      .HasColumnType("varchar(250)")
                      .IsRequired();
            });
        }

    }
}
