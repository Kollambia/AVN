using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Student, string> _studentRepository;
    private IRepository<Faculty, int> _facultyRepository;
    private IRepository<Department, int> _departmentRepository;
    private IRepository<Group, string> _groupRepository;
    private IRepository<Employee, string> _employeeRepository;
    private IRepository<Direction, int> _directionRepository;
    private IRepository<Subject, int> _subjectRepository;
    private IRepository<SubjectEmployee, int> _subjectEmployeeRepository;
    private IRepository<StudentPayment, int> _studentPaymentRepository;
    private IRepository<StudentPaymentDetail, int> _studentPaymentDetailRepository;
    private IRepository<Order, int> _orderRepository;
    private IRepository<AcademicYear, int> _academicYearRepository;
    private IRepository<StudentMovement, int> _studentMovementRepository;
    private IRepository<MovementType, int> _movementTypeRepository;
    private IRepository<OrderType, int> _orderTypeRepository;
    private IRepository<Schedule, int> _scheduleRepository;
    private IRepository<GradeBook, int> _gradeBookRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<Student, string> StudentRepository
    {
        get
        {
            if (_studentRepository == null)
                _studentRepository = new DbRepository<Student, string>(_context);
            return _studentRepository;
        }
    }
    
    public IRepository<Faculty, int> FacultyRepository
    {
        get
        {
            if (_facultyRepository == null)
                _facultyRepository = new DbRepository<Faculty, int>(_context);
            return _facultyRepository;
        }
    }
    
    public IRepository<Department, int> DepartmentRepository
    {
        get
        {
            if (_departmentRepository == null)
                _departmentRepository = new DbRepository<Department, int>(_context);
            return _departmentRepository;
        }
    }
    
    public IRepository<Group, string> GroupRepository
    {
        get
        {
            if (_groupRepository == null)
                _groupRepository = new DbRepository<Group, string>(_context);
            return _groupRepository;
        }
    }
    public IRepository<Employee, string> EmployeeRepository
    {
        get
        {
            if (_employeeRepository == null)
                _employeeRepository = new DbRepository<Employee, string>(_context);
            return _employeeRepository;
        }
    }
    public IRepository<Direction, int> DirectionRepository
    {
        get
        {
            if (_directionRepository == null)
                _directionRepository = new DbRepository<Direction, int>(_context);
            return _directionRepository;
        }
    }
    public IRepository<Subject, int> SubjectRepository
    {
        get
        {
            if (_subjectRepository == null)
                _subjectRepository = new DbRepository<Subject, int>(_context);
            return _subjectRepository;
        }
    }
    public IRepository<SubjectEmployee, int> SubjectEmployeeRepository
    {
        get
        {
            if (_subjectEmployeeRepository == null)
                _subjectEmployeeRepository = new DbRepository<SubjectEmployee, int>(_context);
            return _subjectEmployeeRepository;
        }
    }
    public IRepository<StudentPayment, int> StudentPaymentRepository
    {
        get
        {
            if (_studentPaymentRepository == null)
                _studentPaymentRepository = new DbRepository<StudentPayment, int>(_context);
            return _studentPaymentRepository;
        }
    }
    public IRepository<StudentPaymentDetail, int> StudentPaymentDetailRepository
    {
        get
        {
            if (_studentPaymentDetailRepository == null)
                _studentPaymentDetailRepository = new DbRepository<StudentPaymentDetail, int>(_context);
            return _studentPaymentDetailRepository;
        }
    }

    public IRepository<Order, int> OrderRepository
    {
        get
        {
            if(_orderRepository == null)
                _orderRepository = new DbRepository<Order, int>(_context);
            return _orderRepository;
        }
    }

    public IRepository<AcademicYear, int> AcademicYearRepository
    {
        get
        {
            if(_academicYearRepository == null)
                _academicYearRepository = new DbRepository<AcademicYear, int>(_context);
            return _academicYearRepository;
        }
    }

    public IRepository<StudentMovement, int> StudentMovementRepository
    {
        get
        {
            if(_studentMovementRepository == null)
                _studentMovementRepository = new DbRepository<StudentMovement, int>(_context);
            return _studentMovementRepository;
        }
    }
    public IRepository<MovementType, int> MovementTypeRepository
    {
        get
        {
            if (_movementTypeRepository == null)
                _movementTypeRepository = new DbRepository<MovementType, int>(_context);
            return _movementTypeRepository;
        }
    }
    public IRepository<OrderType, int> OrderTypeRepository
    {
        get
        {
            if (_orderTypeRepository == null)
                _orderTypeRepository = new DbRepository<OrderType, int>(_context);
            return _orderTypeRepository;
        }
    }
    public IRepository<Schedule, int> ScheduleRepository
    {
        get
        {
            if (_scheduleRepository == null)
                _scheduleRepository = new DbRepository<Schedule, int>(_context);
            return _scheduleRepository;
        }
    }
    public IRepository<GradeBook, int> GradeBookRepository
    {
        get
        {
            if (_gradeBookRepository == null)
                _gradeBookRepository = new DbRepository<GradeBook, int>(_context);
            return _gradeBookRepository;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}