using Microsoft.AspNetCore.Mvc;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using System.Threading.Tasks;

namespace AVN.Web.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacultyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Faculty
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.FacultyRepository.GetAllAsync());
        }

        // GET: Faculty/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var faculty = await _unitOfWork.FacultyRepository.GetByIdAsync(id);

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
        public async Task<IActionResult> Create([Bind("Id,FacultyName,FacultyShortName,DeanName")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.FacultyRepository.CreateAsync(faculty);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculty/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var faculty = await _unitOfWork.FacultyRepository.GetByIdAsync(id);
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
                await _unitOfWork.FacultyRepository.UpdateAsync(faculty);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // GET: Faculty/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var faculty = await _unitOfWork.FacultyRepository.GetByIdAsync(id);
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
            var faculty = await _unitOfWork.FacultyRepository.GetByIdAsync(id);
            await _unitOfWork.FacultyRepository.DeleteAsync(faculty);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
