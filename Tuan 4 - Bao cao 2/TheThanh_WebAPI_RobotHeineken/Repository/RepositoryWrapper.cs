using Microsoft.EntityFrameworkCore.Storage;
using TheThanh_WebAPI_RobotHeineken.Data;

namespace TheThanh_WebAPI_RobotHeineken.Repository
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<User> User { get; }
        IRepositoryBase<Role> Role { get; }
        IRepositoryBase<Permission> Permission { get; }
        IRepositoryBase<RolePermission> RolePermission { get; }
        IRepositoryBase<RoleUser> RoleUser { get; }
        IRepositoryBase<RecyclingMachine> RecyclingMachine { get; }
        IRepositoryBase<Location> Location { get; }
        IRepositoryBase<RefreshToken> RefreshToken { get; }
        IRepositoryBase<Gift> Gift { get; }
        IRepositoryBase<RecyclingHistory> RecyclingHistory { get; }
        IRepositoryBase<ContainerFullHistory> ContainerFullHistory { get; }
        IRepositoryBase<QRCode> QRCode { get; }

        Task SaveChangeAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly MyDBContext _db;

        public RepositoryWrapper(MyDBContext db)
        {
            _db = db;
        }

        private IRepositoryBase<RecyclingMachine> RecyclingMachineRepositoryBase;
        public IRepositoryBase<RecyclingMachine> RecyclingMachine => RecyclingMachineRepositoryBase ??= new RepositoryBase<RecyclingMachine>(_db);


        private IRepositoryBase<Location> LocationRepositoryBase;
        public IRepositoryBase<Location> Location => LocationRepositoryBase ??= new RepositoryBase<Location>(_db);


        private IRepositoryBase<User> UserRepositoryBase;
        public IRepositoryBase<User> User => UserRepositoryBase ??= new RepositoryBase<User>(_db);


        public IRepositoryBase<Role> RoleRepositoryBase;
        public IRepositoryBase<Role> Role => RoleRepositoryBase ??= new RepositoryBase<Role>(_db);


        public IRepositoryBase<Permission> PermissionRepositoryBase;
        public IRepositoryBase<Permission> Permission => PermissionRepositoryBase ??= new RepositoryBase<Permission>(_db);


        public IRepositoryBase<RolePermission> RolePermissionRepositoryBase;
        public IRepositoryBase<RolePermission> RolePermission => RolePermissionRepositoryBase ??= new RepositoryBase<RolePermission>(_db);


        public IRepositoryBase<RoleUser> RoleUserRepositoryBase;
        public IRepositoryBase<RoleUser> RoleUser => RoleUserRepositoryBase ??= new RepositoryBase<RoleUser>(_db);


        public IRepositoryBase<RefreshToken> RefreshTokenRepositoryBase;
        public IRepositoryBase<RefreshToken> RefreshToken => RefreshTokenRepositoryBase ??= new RepositoryBase<RefreshToken>(_db);


        public IRepositoryBase<Gift> GiftBase;
        public IRepositoryBase<Gift> Gift => GiftBase ??= new RepositoryBase<Gift>(_db);


        public IRepositoryBase<RecyclingHistory> RecyclingHistoryBase;
        public IRepositoryBase<RecyclingHistory> RecyclingHistory => RecyclingHistoryBase ??= new RepositoryBase<RecyclingHistory>(_db);


        public IRepositoryBase<ContainerFullHistory> ContainerFullHistoryBase;
        public IRepositoryBase<ContainerFullHistory> ContainerFullHistory => ContainerFullHistoryBase ??= new RepositoryBase<ContainerFullHistory>(_db);


        public IRepositoryBase<QRCode> QRCodeBase;
        public IRepositoryBase<QRCode> QRCode => QRCodeBase ??= new RepositoryBase<QRCode>(_db);


        public async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _db.Database.BeginTransactionAsync();
        }
    }
}
