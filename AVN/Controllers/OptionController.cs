using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Controllers
{
    public class OptionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public OptionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> AcademicYearList()
        {
            var years = await unitOfWork.AcademicYearRepository.GetAllAsync();
            var mappedYears = mapper.Map<AcademicYear, AcademicYearVM>(years).ToList();
            return PartialView(mappedYears);
        }

        public IActionResult CreateAcademicYear()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAcademicYear(AcademicYearVM academicYear)
        {
            if (ModelState.IsValid)
            {
                var mappedYear = mapper.Map<AcademicYearVM, AcademicYear>(academicYear);
                mappedYear.Name = mappedYear.YearFrom.ToString() + "-" + mappedYear.YearTo.ToString();
                await unitOfWork.AcademicYearRepository.CreateAsync(mappedYear);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicYear);
        }

        public async Task<IActionResult> EditAcademicYear(int id)
        {
            var group = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            var mappedGroup = mapper.Map<AcademicYear, AcademicYearVM>(group);
            return View(mappedGroup);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAcademicYear(int id, AcademicYearVM academicYear)
        {
            if (id != academicYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedYear = mapper.Map<AcademicYearVM, AcademicYear>(academicYear);
                mappedYear.Name = mappedYear.YearFrom.ToString() + "-" + mappedYear.YearTo.ToString();
                await unitOfWork.AcademicYearRepository.UpdateAsync(mappedYear);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicYear);

        }

        public async Task<IActionResult> DeleteAcademicYear(int id)
        {
            var academicYear = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        public async Task<IActionResult> DeleteAcademicYearConfirmed(int id)
        {
            var academicYear = await unitOfWork.AcademicYearRepository.GetByIdAsync(id);
            await unitOfWork.AcademicYearRepository.DeleteAsync(academicYear);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<List<SelectListItem>> GetAcademicYears()
        {
            var academicYears = (await unitOfWork.AcademicYearRepository.GetAllAsync()).OrderByDescending(x => x.Id);
            return academicYears.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
        }

        public async Task<List<SelectListItem>> GetMovementTypes()
        {
            var movementTypes = await unitOfWork.MovementTypeRepository.GetAllAsync();
            return movementTypes.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
        }
    }
}
