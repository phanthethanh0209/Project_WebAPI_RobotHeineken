using TheThanh_WebAPI_RobotHeineken.Data;

namespace TheThanh_WebAPI_RobotHeineken.Repository
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<RecyclingMachine> RecyclingMachine { get; }
        Task SaveChangeAsync();
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

        public async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
