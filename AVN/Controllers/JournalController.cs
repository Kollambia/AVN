using AVN.Business;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AVN.Controllers
{
    public class JournalController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AppDbContext appDbContext;

        public JournalController(UnitOfWork unitOfWork, AppDbContext appDbContext)
        {
            this.unitOfWork = unitOfWork;
            this.appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(string employeeId)
        {
            var employee = appDbContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeGroups = appDbContext.GroupEmployees
                .Include(eg => eg.Group)
                .Where(eg => eg.EmployeeId == employeeId)
                .ToList();

            var groups = employeeGroups.Select(eg => eg.Group).ToList();

            var journalViewModel = new JournalVM()
            {
                Employee = employee,
                Groups = groups.Select(g => new GroupViewModel()
                {
                    Name = g.GroupName,
                    Students = g.Students.Select(s => new StudentViewModel()
                    {
                        Name = s.Name,
                        Score = appDbContext.Grades.FirstOrDefault(grade => grade.EmployeeId == employeeId && grade.StudentId == s.Id).Value
                    }).ToList()
                }).ToList()
            };

            return View(journalViewModel);
        }
    }
}