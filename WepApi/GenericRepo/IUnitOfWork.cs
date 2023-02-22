namespace WepApi.GenericRepo

{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GenericRepository<T>() where T : class;
        void save();
    }
}
