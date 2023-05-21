using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models.ModelVM;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class DirectionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DirectionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Direction
        public async Task<IActionResult> Index()
        {
            var directions = await unitOfWork.DirectionRepository.GetAllAsync("Department");
            var mappedDirections = mapper.Map<Direction, DirectionVM>(directions);
            return View(mappedDirections);
        }

        // GET: Direction/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var direction = await unitOfWork.DirectionRepository.GetByIdAsync(id);

            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // GET: Direction/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }

        // POST: Direction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DirectionName,DirectionShortName,Description,DirectionNumber,CreditCost,DepartmentId")] DirectionVM direction)
        {
            if (ModelState.IsValid)
            {
                var mappedDirection = mapper.Map<DirectionVM, Direction>(direction);
                await unitOfWork.DirectionRepository.CreateAsync(mappedDirection);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View(direction);
        }

        // GET: Direction/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var direction = await unitOfWork.DirectionRepository.GetByIdAsync(id);
            if (direction == null)
            {
                return NotFound();
            }
            var mappedDirection = mapper.Map<Direction, DirectionVM>(direction);
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View(mappedDirection);
        }

        // POST: Direction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DirectionName,DirectionShortName,Description,DirectionNumber,CreditCost,DepartmentId")] DirectionVM direction)
        {
            if (id != direction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedDirection = mapper.Map<DirectionVM, Direction>(direction);
                await unitOfWork.DirectionRepository.UpdateAsync(mappedDirection);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            return View(direction);
        }

        // GET: Direction/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var direction = await unitOfWork.DirectionRepository.GetByIdAsync(id);
            if (direction == null)
            {
                return NotFound();
            }

            return View(direction);
        }

        // POST: Direction/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direction = await unitOfWork.DirectionRepository.GetByIdAsync(id);
            await unitOfWork.DirectionRepository.DeleteAsync(direction);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
