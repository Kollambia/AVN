using AVN.Model.Entities;

namespace AVN.Data.Repository;

public interface IRepository<T> where T : BaseEntity
{
    public T Create(T entity);
    public T Update(T entity);
    public IQueryable<T> GetAll();
    public T GetById(T id);
    public T Delete(T id);
    public T DeleteById(T id);
    public bool IsExists(T id);
}