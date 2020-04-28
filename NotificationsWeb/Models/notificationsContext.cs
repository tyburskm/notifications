using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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

        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<NotificationInGroup> NotificationInGroup { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<Parameters> Parameters { get; set; }
        public virtual DbSet<PcInGroup> PcInGroup { get; set; }
        public virtual DbSet<Pcs> Pcs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
             optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Groups>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

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

            modelBuilder.Entity<NotificationInGroup>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.NotificationInGroup)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationInGroup_Groups");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.NotificationInGroup)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NotificationInGroup_Notifications");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.RunAtTime).HasColumnType("datetime");

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

            modelBuilder.Entity<PcInGroup>(entity =>
            {
                entity.Property(e => e.Pcid).HasColumnName("PCId");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.PcInGroup)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PcInGroup_Groups");

                entity.HasOne(d => d.Pc)
                    .WithMany(p => p.PcInGroup)
                    .HasForeignKey(d => d.Pcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PcInGroup_PCs");
            });

            modelBuilder.Entity<Pcs>(entity =>
            {
                entity.ToTable("PCs");

                entity.Property(e => e.PcName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
