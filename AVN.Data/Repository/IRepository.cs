using AVN.Model.Entities;
using System.Linq.Expressions;

namespace AVN.Data.Repository
{
    public interface IRepository<T, TId> where T : BaseEntity<T, TId>
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        T GetById(string id);
        Task<T> GetByIdAsync(string id);
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> DeleteRangeAsync(IEnumerable<T> entity);
        Task<T> DeleteByIdAsync(int id); 
        Task<bool> IsExistsAsync(int id);
        void Update(T entity);

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<Group>> GetGroupsByEmployeeIdAsync(string employeeId);
    }
}
