using AutoMapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;

        public EmployeeController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var employees = await unitOfWork.EmployeeRepository.GetAllAsync();
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
            if (ModelState.IsValid)
            {
                var newId = Guid.NewGuid().ToString();
                employee.Id = newId;
                var user = new AppUser() { UserName = employee.Name, Id = newId };
                var mappedEmployee = mapper.Map<EmployeeVM, Employee>(employee);
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleConst.Employee);

                    await unitOfWork.EmployeeRepository.CreateAsync(mappedEmployee);
                    await unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await unitOfWork.EmployeeRepository.UpdateAsync(employee);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employee = await unitOfWork.EmployeeRepository.GetByIdAsync(id);
            await unitOfWork.EmployeeRepository.DeleteAsync(employee);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<List<SelectListItem>> GetEmployees()
        {
            var faculties = await unitOfWork.EmployeeRepository.GetAllAsync();
            var facultyList = faculties.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GetFullName() }).ToList();
            return facultyList;
        }
    }
}
