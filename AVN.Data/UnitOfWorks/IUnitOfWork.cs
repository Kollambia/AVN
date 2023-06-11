using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<Student, string> StudentRepository { get; }
    IRepository<Faculty, int> FacultyRepository { get;  }
    IRepository<Department, int> DepartmentRepository { get;  }
    IRepository<Group, string> GroupRepository { get; }
    IRepository<Employee, string> EmployeeRepository { get; }
    IRepository<Direction, int> DirectionRepository { get; }
    IRepository<Subject, int> SubjectRepository { get; }
    IRepository<SubjectEmployee, int> SubjectEmployeeRepository { get; }
    IRepository<StudentPayment, int> StudentPaymentRepository { get; }
    IRepository<StudentPaymentDetail, int> StudentPaymentDetailRepository { get; }
    IRepository<Order, int> OrderRepository { get; }
    IRepository<AcademicYear, int> AcademicYearRepository { get; }
    IRepository<StudentMovement, int> StudentMovementRepository { get; }
    IRepository<MovementType, int> MovementTypeRepository { get; }
    IRepository<OrderType, int> OrderTypeRepository { get; }
    Task<int> SaveChangesAsync();
}