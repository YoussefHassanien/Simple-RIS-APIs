using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
    {
        protected AppDbContext _context = context;
        public async Task<T?> GetById(uint id, string[]? includes = null) 
        {
            IQueryable<T> query = _context.Set<T>();

            if(includes != null && includes.Length > 0) 
            {
                foreach (var include in includes) 
                {
                    query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task<T?> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
             _context.Set<T>().Update(entity);
            return entity;
        }

        public async Task<bool> Delete(uint id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);
            return true;
        }

    }
}
