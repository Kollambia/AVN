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
        private readonly AppDbContext context;

        public EmployeeController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
            this.context = context;
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

                var user = new AppUser()
                {
                    UserName = employee.Email, 
                    Id = newId, 
                    FullName = employee.FullName, 
                    Email = employee.Email
                };

                try
                {
                    var mappedEmployee = mapper.Map<EmployeeVM, Employee>(employee);
                    var result = await userManager.CreateAsync(user, employee.Password);
                    if (!result.Succeeded)
                    {
                        TempData["error"] = $"Произошла ошибка создания пользователя: {result.Errors}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                        return RedirectToAction("Create", "Employee");
                    }
                    await userManager.AddToRoleAsync(user, RoleConst.Employee);

                    await unitOfWork.EmployeeRepository.CreateAsync(mappedEmployee);
                    await unitOfWork.SaveChangesAsync();


                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Employee");
                }
                
            }
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var employee = await context.Employees
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
            try
            {
                var employee = await context.Employees
                    .Include(s => s.Subjects)
                    .Include(g => g.GroupEmployees)
                    .Include(s => s.Schedules)
                    .FirstOrDefaultAsync(i => i.Id == id);

                TempData["success"] = "Запись успешно удалена";
                await unitOfWork.SubjectRepository.DeleteRangeAsync(employee.Subjects);
                context.GroupEmployees.RemoveRange(employee.GroupEmployees);
                await unitOfWork.ScheduleRepository.DeleteRangeAsync(employee.Schedules);

                await unitOfWork.EmployeeRepository.DeleteAsync(employee);
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        // Обрабатываем ошибки, возникшие в процессе удаления пользователя
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }

                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Employee");
            }
            
        }

        public async Task<List<SelectListItem>> GetEmployees()
        {
            var employees= await unitOfWork.EmployeeRepository.GetAllAsync();
            var employeeList = employees.Select(f => new SelectListItem { Value = f.Id, Text = f.GetFullName() }).ToList();
            return employeeList;
        }
    }
}
