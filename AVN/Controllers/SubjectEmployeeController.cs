using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class SubjectEmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public SubjectEmployeeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: SubjectEmployee
        public async Task<IActionResult> Index()
        {
            return View(await unitOfWork.SubjectEmployeeRepository.GetAllAsync());
        }

        // GET: SubjectEmployee/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subjectEmployee = await unitOfWork.SubjectEmployeeRepository.GetByIdAsync(id);
            if (subjectEmployee == null)
            {
                return NotFound();
            }

            return View(subjectEmployee);
        }

        // GET: SubjectEmployee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubjectEmployee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,EmployeeId,EmployeeCreditHours,Id")] SubjectEmployee subjectEmployee)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.SubjectEmployeeRepository.CreateAsync(subjectEmployee);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectEmployee);
        }

        // GET: SubjectEmployee/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subjectEmployee = await unitOfWork.SubjectEmployeeRepository.GetByIdAsync(id);
            if (subjectEmployee == null)
            {
                return NotFound();
            }
            return View(subjectEmployee);
        }

        // POST: SubjectEmployee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,EmployeeId,EmployeeCreditHours,Id")] SubjectEmployee subjectEmployee)
        {
            if (id != subjectEmployee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await unitOfWork.SubjectEmployeeRepository.UpdateAsync(subjectEmployee);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectEmployee);
        }

        // GET: SubjectEmployee/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subjectEmployee = await unitOfWork.SubjectEmployeeRepository.GetByIdAsync(id);
            if (subjectEmployee == null)
            {
                return NotFound();
            }

            return View(subjectEmployee);
        }

        // POST: SubjectEmployee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await unitOfWork.SubjectEmployeeRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
