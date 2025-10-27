using Microsoft.EntityFrameworkCore;
using CarManagementAPI.Models;


namespace CarManagementAPI.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(c => c.CarId);

                entity.Property(c => c.Brand)
                  .IsRequired()
                  .HasMaxLength(50);

                entity.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(c => c.Year)
                   .IsRequired();

               
                entity.Property(c => c.Price)
                    .HasColumnType("decimal(18,2)") 
                    .IsRequired();

                entity.Property(c => c.Mileage)
                    .IsRequired();

                entity.Property(c => c.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(c => c.Dealer)         
                    .WithMany(d => d.Cars)             
                    .HasForeignKey(c => c.DealerId)    
                    .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Dealer>(entity =>
            {
                entity.HasKey(d => d.DealerId);

                entity.Property(d=>d.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(d=>d.Address)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(d=>d.Phone)
                .HasMaxLength(12);

                entity.Property(d=>d.Email)
                .HasMaxLength (50);
            });
        }
    }
}
