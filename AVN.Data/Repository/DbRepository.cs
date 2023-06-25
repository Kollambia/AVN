using System.Linq.Expressions;
using AVN.Model.Entities;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.EntityFrameworkCore;

namespace AVN.Data.Repository
{
    public class DbRepository<T, TId> : IRepository<T, TId> where T : BaseEntity<T, TId>
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

        public IEnumerable<T> GetAll()
        {
            return  _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        //public async Task<IEnumerable<T>> GetAllAsync(params string[] includes)
        //{
        //    var query = _context.Set<T>().AsQueryable();

        //    foreach (var include in includes)
        //    {
        //        query = query.Include(include);
        //    }

        //    return await query.ToListAsync();
        //}

        public T GetById(int id)
        {
            return  _context.Set<T>()
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public T GetById(string id)
        {
            return _context.Set<T>()
                .AsNoTracking()
                .FirstOrDefault(en => en.Id.Equals(id));
        }

        public async Task<T> GetByIdAsync(string id)
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
        public async Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
            return entities;
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

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsByEmployeeIdAsync(string employeeId)
        {
            return await _context.Set<GroupEmployee>()
                .Where(ge => ge.EmployeeId == employeeId)
                .Select(ge => ge.Group)
                .ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}