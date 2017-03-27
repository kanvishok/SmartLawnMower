using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LawnMower.Infrastructure.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        
        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity.Id;
        }

        public virtual int Edit(T entity)
        {
            DbEntityEntry dbEntityEntry = _context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
            return entity.Id;
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual T GetSingle(int id)
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
