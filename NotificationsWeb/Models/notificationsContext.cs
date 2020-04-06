using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NotificationsWeb.Models
{
    public partial class notificationsContext : DbContext
    {
        public notificationsContext()
        {
        }

        public notificationsContext(DbContextOptions<notificationsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=hutisd0kwisql70;Database=notifications;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ValidFrom)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ValidTo)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(dateadd(hour,(1),getdate()))");
            });

            modelBuilder.Entity<Parameters>(entity =>
            {
                entity.Property(e => e.BackgroundColor).HasMaxLength(50);

                entity.Property(e => e.Gradient).HasMaxLength(50);

                entity.Property(e => e.NotificationText)
                    .IsRequired()
                    .HasDefaultValueSql("(N'')");

                entity.Property(e => e.TextColor).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("(N'Info')");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parameters_Notifications");
            });
        }
    }
}
