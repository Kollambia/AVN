using Microsoft.AspNetCore.Mvc;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using System.Threading.Tasks;

namespace AVN.Web.Controllers
{
    public class DirectionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DirectionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Direction
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.DirectionRepository.GetAllAsync());
        }

        // GET: Direction/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var direction = await _unitOfWork.DirectionRepository.GetByIdAsync(id);

            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // GET: Direction/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Direction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DirectionName,DirectionShortName,Description,DirectionNumber,CreditCost,DepartmentId")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DirectionRepository.CreateAsync(direction);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direction);
        }

        // GET: Direction/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var direction = await _unitOfWork.DirectionRepository.GetByIdAsync(id);
            if (direction == null)
            {
                return NotFound();
            }
            return View(direction);
        }

        // POST: Direction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DirectionName,DirectionShortName,Description,DirectionNumber,CreditCost,DepartmentId")] Direction direction)
        {
            if (id != direction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.DirectionRepository.UpdateAsync(direction);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(direction);
        }

        // GET: Direction/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var direction = await _unitOfWork.DirectionRepository.GetByIdAsync(id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // POST: Direction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direction = await _unitOfWork.DirectionRepository.GetByIdAsync(id);
            await _unitOfWork.DirectionRepository.DeleteAsync(direction);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
