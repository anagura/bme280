using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bme280_viewer.Models
{
    public partial class bme280Context : DbContext
    {
        public bme280Context()
        {
        }

        public bme280Context(DbContextOptions<bme280Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bme280> Bme280 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=<IP Adress>;Database=<database>;User Id=<user id>;Password=<password>;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Bme280>(entity =>
            {
                entity.ToTable("Bme280", "bme280");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Created).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Hum)
                    .HasColumnName("hum")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("0.00");

                entity.Property(e => e.Pressure)
                    .HasColumnName("pressure")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("0.00");

                entity.Property(e => e.Temperature)
                    .HasColumnName("temperature")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("0.00");
            });
        }
    }
}
