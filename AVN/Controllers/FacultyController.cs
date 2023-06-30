using AVN.Automapper;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Web.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public FacultyController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Faculty
        public async Task<IActionResult> Index()
        {
            var faculties = await unitOfWork.FacultyRepository.GetAllAsync();
            var mappedFaculties = mapper.Map<Faculty, FacultyVM>(faculties);
            return View(mappedFaculties);
        }

        // GET: Faculty/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var faculty = await unitOfWork.FacultyRepository.GetByIdAsync(id);

            if (faculty == null)
            {
                return NotFound();
            }
            var mappedFaculty = mapper.Map<Faculty, FacultyVM>(faculty);
            return View(mappedFaculty);
        }

        // GET: Faculty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faculty/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacultyName,FacultyShortName,DeanName")] FacultyVM faculty)
        {
            if (ModelState.IsValid)
            {
                var mappedFaculty = mapper.Map<FacultyVM, Faculty>(faculty);
                await unitOfWork.FacultyRepository.CreateAsync(mappedFaculty);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculty/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await unitOfWork.FacultyRepository.GetByIdAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            var mappedFaculty = mapper.Map<Faculty, FacultyVM>(faculty);
            return View(mappedFaculty);
        }

        // POST: Faculty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,FacultyShortName,DeanName")] FacultyVM faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedFaculty = mapper.Map<FacultyVM, Faculty>(faculty);
                await unitOfWork.FacultyRepository.UpdateAsync(mappedFaculty);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculty/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await unitOfWork.FacultyRepository.GetByIdAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            var mappedFaculty = mapper.Map<Faculty, FacultyVM>(faculty);
            return View(mappedFaculty);
        }

        // POST: Faculty/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var faculty = await context.Faculties
                    .Include(d => d.Departments)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (faculty == null)
                {
                    TempData["error"] =
                        "Не удалось найти факультеты. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Faculty");
                }

                if (faculty.Departments.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите кафедры связанные с факультетом";
                    return RedirectToAction("Index", "Faculty");
                }
                TempData["success"] = "Запись успешно удалена";

                await unitOfWork.FacultyRepository.DeleteAsync(faculty);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Faculty");
            }
            
        }

        public async Task<List<SelectListItem>> GetFaculties()
        {
            var faculties = await unitOfWork.FacultyRepository.GetAllAsync();
            var facultyList = faculties.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.FacultyName }).ToList();
            return facultyList;
        }
    }
}
