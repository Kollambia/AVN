using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Student> _studentRepository;
    private IRepository<Faculty> _facultyRepository;
    private IRepository<Department> _departmentRepository;
    private IRepository<Group> _groupRepository;
    private IRepository<Employee> _employeeRepository;
    private IRepository<Direction> _directionRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IRepository<Student> StudentRepository
    {
        get
        {
            if (_studentRepository == null)
                _studentRepository = new DbRepository<Student>(_context);
            return _studentRepository;
        }
    }
    
    public IRepository<Faculty> FacultyRepository
    {
        get
        {
            if (_facultyRepository == null)
                _facultyRepository = new DbRepository<Faculty>(_context);
            return _facultyRepository;
        }
    }
    
    public IRepository<Department> DepartmentRepository
    {
        get
        {
            if (_departmentRepository == null)
                _departmentRepository = new DbRepository<Department>(_context);
            return _departmentRepository;
        }
    }
    
    public IRepository<Group> GroupRepository
    {
        get
        {
            if (_groupRepository == null)
                _groupRepository = new DbRepository<Group>(_context);
            return _groupRepository;
        }
    }
    public IRepository<Employee> EmployeeRepository
    {
        get
        {
            if (_employeeRepository == null)
                _employeeRepository = new DbRepository<Employee>(_context);
            return _employeeRepository;
        }
    }
    public IRepository<Direction> DirectionRepository
    {
        get
        {
            if (_directionRepository == null)
                _directionRepository = new DbRepository<Direction>(_context);
            return _directionRepository;
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