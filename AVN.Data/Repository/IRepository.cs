using AVN.Model.Entities;
using System.Linq.Expressions;

namespace AVN.Data.Repository
{
    public interface IRepository<T, TId> where T : BaseEntity<T, TId>
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T> GetByIdAsync(int id); 
        Task<T> GetByIdAsync(string id);
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteByIdAsync(int id); 
        Task<bool> IsExistsAsync(int id);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
    }
}