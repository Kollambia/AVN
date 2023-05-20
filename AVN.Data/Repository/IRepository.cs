using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AVN.Model.Entities;

namespace AVN.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(); 
        Task<T> GetByIdAsync(int id); 
        Task<T> DeleteAsync(T entity);
        Task<T> DeleteByIdAsync(int id); 
        Task<bool> IsExistsAsync(int id);
    }
}