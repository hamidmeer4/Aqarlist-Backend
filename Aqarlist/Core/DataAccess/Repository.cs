using Aqarlist.Core.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Aqarlist.Core.DataBase
{
    public class Repository<T, Tkey> : IRepository<T, Tkey> where T : class
    {
        private readonly DbSet<T> _table;
        private readonly ApiDbContext _db;
        public Repository(ApiDbContext db)
        {
            _db = db;
            _table = db.Set<T>();
        }
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Tkey id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tkey id)
        {
            throw new NotImplementedException();
        }
    }
}
