using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_language.Data
{
    public class EfGenericAsyncRepository<T> : IRepository<T> where T : class
    {
        protected IDbContext Context { get; set; }

        public EfGenericAsyncRepository(IDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> All()
        {
            return Context.Set<T>().AsQueryable();
        }

        public async Task<ICollection<T>> AllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public T GetById(object id)
        {
            return Context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        //public TObject Find(Expression<Func<TObject, bool>> match)
        //{
        //    return _context.Set<TObject>().SingleOrDefault(match);
        //}

        //public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match)
        //{
        //    return await _context.Set<TObject>().SingleOrDefaultAsync(match);
        //}

        //public ICollection<TObject> FindAll(Expression<Func<TObject, bool>> match)
        //{
        //    return _context.Set<TObject>().Where(match).ToList();
        //}

        //public async Task<ICollection<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match)
        //{
        //    return await _context.Set<TObject>().Where(match).ToListAsync();
        //}

        public virtual void Add(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.Context.Set<T>().Add(entity);
            }
        }

    }
}
