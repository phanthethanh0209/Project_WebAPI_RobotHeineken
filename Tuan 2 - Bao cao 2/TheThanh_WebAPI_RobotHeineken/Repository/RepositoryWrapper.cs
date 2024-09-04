using Microsoft.EntityFrameworkCore.Storage;
using TheThanh_WebAPI_RobotHeineken.Data;

namespace TheThanh_WebAPI_RobotHeineken.Repository
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<RecyclingMachine> RecyclingMachine { get; }
        IRepositoryBase<Location> Location { get; }
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
