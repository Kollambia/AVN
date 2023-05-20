using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAllAsync("Faculty");
            var mappedDepartments = mapper.Map<Department, DepartmentVM>(departments);
            return View(mappedDepartments);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName,DepartmentShortName,FacultyId")] Department department)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.DepartmentRepository.CreateAsync(department);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,DepartmentShortName,FacultyId")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await unitOfWork.DepartmentRepository.UpdateAsync(department);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            await unitOfWork.DepartmentRepository.DeleteAsync(department);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
