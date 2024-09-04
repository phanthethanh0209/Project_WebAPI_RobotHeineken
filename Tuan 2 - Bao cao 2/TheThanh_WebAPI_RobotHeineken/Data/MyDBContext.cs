using Microsoft.EntityFrameworkCore;

namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Location> Locations { get; set; }
        public DbSet<RobotType> RobotTypes { get; set; }
        public DbSet<Robot> Robots { get; set; }
        public DbSet<RecyclingMachine> RecyclingMachines { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Location
            modelBuilder.Entity<Location>(e =>
            {
                e.ToTable("Location");
                e.HasKey(e => e.LocationID);
                e.HasIndex(e => e.LocationName).IsUnique();
                e.Property(r => r.CreateAt).HasDefaultValueSql("GETDATE()");
                e.Property(l => l.Latitude).HasColumnType("decimal(18, 6)");
                e.Property(l => l.Longitude).HasColumnType("decimal(18, 6)");
            });

            // RobotType
            modelBuilder.Entity<RobotType>(e =>
            {
                e.ToTable("RobotType");
                e.HasKey(e => e.TypeID);
            });

            // RecyclingMechine
            modelBuilder.Entity<RecyclingMachine>(e =>
            {
                e.ToTable("RecyclingMachine");
                e.HasKey(dh => dh.MachineID);
                e.HasIndex(dh => dh.MachineCode).IsUnique();
                e.Property(dh => dh.Status).HasDefaultValue(1);
                e.Property(dh => dh.ContainerStatus).HasDefaultValue(0);
                e.Property(r => r.CreateAt).HasDefaultValueSql("GETDATE()");

                // test
                e.Property(r => r.LocationID).IsRequired(false); // Chỉ định LocationID không bắt buộc

                e.HasOne(e => e.Location)
                    .WithMany(e => e.RecyclingMachines)
                    .HasForeignKey(e => e.LocationID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Robot
            modelBuilder.Entity<Robot>(e =>
            {
                e.ToTable("Robot");
                e.HasKey(dh => dh.RobotID);
                e.Property(dh => dh.Status).HasDefaultValue(1);
                e.Property(dh => dh.BatteryLevel).HasDefaultValue(100);

                e.HasOne(e => e.Location)
                    .WithMany(e => e.Robots)
                    .HasForeignKey(e => e.LocationID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
