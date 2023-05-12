using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Student> StudentRepository { get; }
    IRepository<Faculty> FacultyRepository { get;  }
    IRepository<Department> DepartmentRepository { get;  }
    IRepository<Group> GroupRepository { get; }
    Task<int> SaveChangesAsync();
}