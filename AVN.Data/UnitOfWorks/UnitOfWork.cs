using System.Runtime.CompilerServices;
using AVN.Data.Repository;
using AVN.Model.Entities;
using Microsoft.EntityFrameworkCore;

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
    private IRepository<Subject> _subjectRepository;
    private IRepository<SubjectEmployee> _subjectEmployeeRepository;
    private IRepository<StudentPayment> _studentPaymentRepository;
    private IRepository<StudentPaymentDetail> _studentPaymentDetailRepository;
    private IRepository<Order> _orderRepository;

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
    public IRepository<Subject> SubjectRepository
    {
        get
        {
            if (_subjectRepository == null)
                _subjectRepository = new DbRepository<Subject>(_context);
            return _subjectRepository;
        }
    }
    public IRepository<SubjectEmployee> SubjectEmployeeRepository
    {
        get
        {
            if (_subjectEmployeeRepository == null)
                _subjectEmployeeRepository = new DbRepository<SubjectEmployee>(_context);
            return _subjectEmployeeRepository;
        }
    }
    public IRepository<StudentPayment> StudentPaymentRepository
    {
        get
        {
            if (_studentPaymentRepository == null)
                _studentPaymentRepository = new DbRepository<StudentPayment>(_context);
            return _studentPaymentRepository;
        }
    }
    public IRepository<StudentPaymentDetail> StudentPaymentDetailRepository
    {
        get
        {
            if (_studentPaymentDetailRepository == null)
                _studentPaymentDetailRepository = new DbRepository<StudentPaymentDetail>(_context);
            return _studentPaymentDetailRepository;
        }
    }

    public IRepository<Order> OrderRepository
    {
        get
        {
            if(_orderRepository == null)
                _orderRepository = new DbRepository<Order>(_context);
            return _orderRepository;
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