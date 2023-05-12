using AVN.Data.Repository;
using AVN.Model.Entities;

namespace AVN.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IRepository<Student> _studentRepository;
    private IRepository<Faculty> _facultyRepository;

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

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}