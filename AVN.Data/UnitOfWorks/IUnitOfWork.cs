using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Student> StudentRepository { get; }
    IRepository<Faculty> FacultyRepository { get;  }
    IRepository<Department> DepartmentRepository { get;  }
    IRepository<Group> GroupRepository { get; }
    IRepository<Employee> EmployeeRepository { get; }
    IRepository<Direction> DirectionRepository { get; }
    IRepository<Subject> SubjectRepository { get; }
    IRepository<SubjectEmployee> SubjectEmployeeRepository { get; }
    Task<int> SaveChangesAsync();
}