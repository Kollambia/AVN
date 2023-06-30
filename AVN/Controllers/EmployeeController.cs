using AutoMapper;
using AVN.Business;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static AVN.Web.Controllers.StudentController;

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
            return View();
        }

        public async Task<ActionResult> EmployeeList(int facultyId, int departmentId, string fullname)
        {
            try
            {
                var employees = await unitOfWork.EmployeeRepository.GetAllAsync();
                if (!string.IsNullOrEmpty(fullname))
                {
                    var studentOrderService = new OrderService(context);
                    employees = studentOrderService.GetEmployeesByFullName(fullname);
                    return PartialView(employees.Select(s => mapper.Map<Employee, EmployeeVM>(s)) ?? new List<EmployeeVM>());
                }
                else if (departmentId > 0)
                {
                    employees = employees.Where(x => x.DepartmentId == departmentId);
                }
                else if (facultyId > 0)
                {
                    employees = employees.Where(x => x.Department?.FacultyId == facultyId);
                }
                else
                {
                    return PartialView(new List<EmployeeVM>());
                }

                var mappedEmployees = employees.Select(s => mapper.Map<Employee, EmployeeVM>(s)).ToList();
                return PartialView(mappedEmployees ?? new List<EmployeeVM>());
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Employee");
            }

        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var employee = await context.Employees
                .Include(d => d.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            var mappedEmployee = mapper.Map<Employee, EmployeeVM>(employee);
            return View(mappedEmployee);
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
            if (ModelState.IsValid)
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

                userManager.PasswordValidators.Clear();
                userManager.PasswordValidators.Add(new CustomPasswordValidator<AppUser>());

                try
                {
                    var mappedEmployee = mapper.Map<EmployeeVM, Employee>(employee);

                    var result = await userManager.CreateAsync(user, employee.Password);
                    if (!result.Succeeded)
                    {
                        TempData["error"] = $"Произошла ошибка создания пользователя: {result.Errors}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                        return RedirectToAction("Create", "Employee");
                    }
                    else if (!result.Succeeded)
                    {
                        TempData["error"] = $"Ошибка при создании учетной записи работника: {result.Errors.First().Description}";
                        return View(employee);
                    }

                    await userManager.AddToRoleAsync(user, RoleConst.Employee);

                    await unitOfWork.EmployeeRepository.CreateAsync(mappedEmployee);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно добавлена";
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
            var mappedEmployee = mapper.Map<Employee, EmployeeEditVM>(employee);
            if (employee == null)
            {
                return NotFound();
            }
            return View(mappedEmployee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EmployeeEditVM employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedEmployee = mapper.Map<EmployeeEditVM, Employee>(employeeVM);
                    await unitOfWork.EmployeeRepository.UpdateAsync(mappedEmployee);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction(nameof(Index));
                }
                return View(employeeVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Employee");
            }
           
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

                if (employee.GroupEmployees.Any())
                {
                    TempData["error"] = "Не удалось удалить запись.";
                    return RedirectToAction("Index", "Employee");
                }

                if (employee.Subjects.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите предметы связанные с работником";
                    return RedirectToAction("Index", "Employee");
                }

                if (employee.Schedules.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите расписание связанные с работником";
                    return RedirectToAction("Index", "Employee");
                }

                TempData["success"] = "Запись успешно удалена";
                
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

        public async Task<List<SelectListItem>> GetEmployeesByDeparment(int departmentId)
        {
            var employees = (await unitOfWork.EmployeeRepository.GetAllAsync()).Where(x => x.DepartmentId == departmentId);
            var employeeList = employees.Select(f => new SelectListItem { Value = f.Id, Text = f.GetFullName() }).ToList();
            return employeeList;
        }

        public async Task<SelectListItem> GetEmployeeBySubject(int subjectId)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(subjectId);
            if (subject != null && subject.Employee != null)
            {
                return new SelectListItem { Value = subject.Employee.Id, Text = subject.Employee.FullName };
            }
            return null;
        }

    }
}
