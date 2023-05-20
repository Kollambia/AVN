using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FacultyController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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

            return View(faculty);
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
            return View(faculty);
        }

        // POST: Faculty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,FacultyShortName,DeanName")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await unitOfWork.FacultyRepository.UpdateAsync(faculty);
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

            return View(faculty);
        }

        // POST: Faculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await unitOfWork.FacultyRepository.GetByIdAsync(id);
            await unitOfWork.FacultyRepository.DeleteAsync(faculty);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
