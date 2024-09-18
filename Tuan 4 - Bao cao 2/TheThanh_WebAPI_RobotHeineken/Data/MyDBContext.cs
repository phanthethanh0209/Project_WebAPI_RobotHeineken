using Microsoft.EntityFrameworkCore;

namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RoleUser> RoleUsers { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RecyclingMachine> RecyclingMachines { get; set; }
        public DbSet<RecyclingHistory> RecyclingHistories { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
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

            // RecyclingMachine
            modelBuilder.Entity<RecyclingMachine>(e =>
            {
                e.ToTable("RecyclingMachine");
                e.HasKey(dh => dh.MachineID);
                e.HasIndex(dh => dh.MachineCode).IsUnique();
                e.Property(dh => dh.Status).HasDefaultValue(1);
                e.Property(dh => dh.ContainerStatus).HasDefaultValue(0);
                e.Property(r => r.CreateAt).HasDefaultValueSql("GETDATE()");
                e.Property(r => r.DeploymentDate).HasDefaultValue(null);


                e.Property(r => r.LocationID).IsRequired(false); // Chỉ định LocationID không bắt buộc

                e.HasOne(e => e.Location)
                    .WithMany(e => e.RecyclingMachines)
                    .HasForeignKey(e => e.LocationID)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            // RecyclingHistory
            modelBuilder.Entity<RecyclingHistory>(e =>
            {
                e.ToTable("RecyclingHistory");
                e.HasKey(dh => dh.HistoryID);
                e.Property(r => r.ApproachTime).HasDefaultValueSql("GETDATE()");

                e.HasOne(e => e.RecyclingMachine)
                    .WithMany(e => e.RecyclingHistories)
                    .HasForeignKey(e => e.MachineID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ContainerFullHistory
            modelBuilder.Entity<ContainerFullHistory>(e =>
            {
                e.ToTable("ContainerFullHistory");
                e.HasKey(dh => dh.ContainerID);
                e.Property(r => r.FullTime).HasDefaultValueSql("GETDATE()");

                e.HasOne(e => e.RecyclingMachine)
                    .WithMany(e => e.ContainerFullHistories)
                    .HasForeignKey(e => e.MachineID)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // User
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(dh => dh.UserID);
                e.HasIndex(dh => dh.Email).IsUnique();
            });

            // Role
            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Role");
                e.HasKey(dh => dh.RoleID);

                //e.HasData(
                //    new Role { RoleID = 1, Name = "System User", Note = "Chỉ được xem" },
                //    new Role { RoleID = 2, Name = "PG Admin", Note = "Toàn quyền" }
                //    );
            });

            // Permission
            modelBuilder.Entity<Permission>(e =>
            {
                e.ToTable("Permission");
                e.HasKey(dh => dh.PermissionID);
                e.HasData(
                    new Permission { PermissionID = 1, Name = "Create" },
                    new Permission { PermissionID = 2, Name = "Delete" },
                    new Permission { PermissionID = 3, Name = "Update" },
                    new Permission { PermissionID = 4, Name = "GetAll" },
                    new Permission { PermissionID = 5, Name = "GetSingle" }
                    );
            });

            // RoleUser
            modelBuilder.Entity<RoleUser>(e =>
            {
                e.ToTable("RoleUser");
                e.HasKey(r => new { r.RoleID, r.UserID });

                e.HasOne(r => r.Users)
                    .WithMany(e => e.RoleUsers)
                    .HasForeignKey(e => e.UserID);

                e.HasOne(r => r.Roles)
                    .WithMany(e => e.RoleUsers)
                    .HasForeignKey(e => e.RoleID);
            });

            // RolePermission
            modelBuilder.Entity<RolePermission>(e =>
            {
                e.ToTable("RolePermission");
                e.HasKey(r => new { r.RoleID, r.PermissionID });

                e.HasOne(r => r.Roles)
                    .WithMany(e => e.RolePermissions)
                    .HasForeignKey(e => e.RoleID);

                e.HasOne(r => r.Permissions)
                    .WithMany(e => e.RolePermissions)
                    .HasForeignKey(e => e.PermissionID);

                //e.HasData(
                //    new RolePermission { RoleID = 1, PermissionID = 4 },
                //    new RolePermission { RoleID = 2, PermissionID = 1 },
                //    new RolePermission { RoleID = 2, PermissionID = 2 },
                //    new RolePermission { RoleID = 2, PermissionID = 3 },
                //    new RolePermission { RoleID = 2, PermissionID = 4 }
                //    );
            });

            // RefreshToken
            modelBuilder.Entity<RefreshToken>(e =>
            {
                e.ToTable("RefreshToken");
                e.HasKey(r => new { r.Id });

                e.HasOne(r => r.Users)
                    .WithMany(e => e.RefreshTokens)
                    .HasForeignKey(e => e.UserId);
            });

            // Gift
            modelBuilder.Entity<Gift>(e =>
            {
                e.ToTable("Gift");
                e.HasKey(dh => dh.GiftID);
                e.Property(dh => dh.IsActive).HasDefaultValue(1);
            });

            // QRCode
            modelBuilder.Entity<QRCode>(e =>
            {
                e.ToTable("QRCode");
                e.HasKey(dh => dh.Code);
                e.Property(r => r.StartTime).HasDefaultValueSql("GETDATE()");
                e.Property(r => r.TimeUsed).HasDefaultValue(null);
                e.Property(dh => dh.IsActive).HasDefaultValue(1);

                e.HasOne(q => q.RecyclingHistory)
                    .WithOne(r => r.QRCode)
                    .HasForeignKey<QRCode>(r => r.HistoryID);

                e.HasOne(q => q.Gift)
                    .WithMany(g => g.QRCodes)
                    .HasForeignKey(q => q.GiftID)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(q => q.User)
                    .WithMany(u => u.QRCodes)
                    .HasForeignKey(q => q.UserID)
                    .IsRequired(false); // Xác định UserID là null
            });

        }
    }
}
