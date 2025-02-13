namespace Aqarlist.Core.DataBase
{
    public interface IRepository<T, in TKey> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(TKey id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(TKey id);
        Task<int> SaveChangesAsync();
    }
}
