using AVN.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AVN.Data.Repository;

public class DbRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public DbRepository(AppDbContext context)
    {
        _context = context;
    }
    public T Create(T entity)
    {
        var createEntity = _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return createEntity.Entity;
    }

    public T Update(T entity)
    {
        var a = _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return a.Entity;
    }

    public IQueryable<T> GetAll()
    {
        if (_context.Set<T>() == null)
        {
            return null;
        }

        var list = _context.Set<T>();

        return list;
    }

    public T GetById(T id)
    {
        if (_context.Set<T>() == null)
        {
            return null;
        }

        var entityId = _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefault(en => en.Id.Equals(id));
        return entityId;
    }

    public T Delete(T id)
    {
        var deleteEntity = _context.Set<T>()
            .Find(id);
        _context.SaveChanges();

        return deleteEntity;
    }

    public T DeleteById(T id)
    {
        var entity = GetById(id);

        _context.Set<T>().Remove((T)entity);

        _context.SaveChanges();

        return entity;
    }

    public bool IsExists(T id)
    {
        return _context.Set<T>().Any(entity => entity.Id.Equals(id));
    }
}