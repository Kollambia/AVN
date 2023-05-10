using AVN.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AVN.Data.Repository
{
    public class DbRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public DbRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var createEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return createEntity.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var updatedEntity = _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public IQueryable GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(en => en.Id.Equals(id));
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var deleteEntity = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return deleteEntity.Entity;
        }

        public async Task<T> DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(entity => entity.Id.Equals(id));
        }
    }
}