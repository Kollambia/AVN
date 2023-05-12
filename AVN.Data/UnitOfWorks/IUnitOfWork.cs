using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Student> StudentRepository { get; }
    IRepository<Faculty> FacultyRepository { get; }
    Task<int> SaveChangesAsync();
}