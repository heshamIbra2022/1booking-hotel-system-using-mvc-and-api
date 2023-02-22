using WepApi.ContextModel;

namespace WepApi.GenericRepo
{
    public class UnitOfWork:IUnitOfWork
    {
        public readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            IGenericRepository<T> Repo = new GenericRepository<T>(_context);
            return Repo;
        }

        public void save()
        {
            _context.SaveChanges();
        }
    }
}
