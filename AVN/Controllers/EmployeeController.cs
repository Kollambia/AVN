using AutoMapper;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly AppDbContext appDbContext;

        public EmployeeController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, AppDbContext appDbContext)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
            this.appDbContext = appDbContext;
        }

        // GET: Employee
        public  IActionResult Index()
        {
            var employees = unitOfWork.EmployeeRepository.GetAll();
            return View(employees);
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM employee)
        {
            if (!ModelState.IsValid)
            {
                var newId = Guid.NewGuid().ToString();
                employee.Id = newId;

                var user = new AppUser() { UserName = employee.Email, Id = newId, FullName = employee.FullName};
                var mappedEmployee = mapper.Map<EmployeeVM, Employee>(employee);
                var result = await userManager.CreateAsync(user, employee.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleConst.Employee);

                    await unitOfWork.EmployeeRepository.CreateAsync(mappedEmployee);
                    await unitOfWork.SaveChangesAsync();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var employee = await appDbContext.Employees
                .Include(d => d.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
            var mappedEmployee = mapper.Map<Employee, EmployeeVM>(employee);
            if (employee == null)
            {
                return NotFound();
            }
            return View(mappedEmployee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EmployeeVM employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var mappedEmployee = mapper.Map<EmployeeVM, Employee>(employeeVM);
                await unitOfWork.EmployeeRepository.UpdateAsync(mappedEmployee);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
            await unitOfWork.EmployeeRepository.DeleteAsync(employee);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<List<SelectListItem>> GetEmployees()
        {
            var employees= await unitOfWork.EmployeeRepository.GetAllAsync();
            var employeeList = employees.Select(f => new SelectListItem { Value = f.Id, Text = f.GetFullName() }).ToList();
            return employeeList;
        }
    }
}
