using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DirectionList(int facultyId = 0)
        {
            var directions = await unitOfWork.DirectionRepository.GetAllAsync();
            if (facultyId > 0)
                directions = directions.Where(x => x.Department.FacultyId == facultyId);

            var mappedDirections = mapper.Map<Direction, DirectionVM>(directions).ToList();
            return PartialView(mappedDirections);
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Direction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DirectionVM direction)
        {
            if (ModelState.IsValid)
            {
                var mappedDirection = mapper.Map<DirectionVM, Direction>(direction);
                await unitOfWork.DirectionRepository.CreateAsync(mappedDirection);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            mappedDirection.FacultyId = direction.Department.FacultyId;
            mappedDirection.DepartmentId = direction.DepartmentId;
            return View(mappedDirection);
        }

        // POST: Direction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DirectionVM direction)
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

        public async Task<List<SelectListItem>> GetDirectionsByDepartment(int departmentId)
        {
            var directions = (await unitOfWork.DirectionRepository.GetAllAsync()).Where(x => x.DepartmentId == departmentId);
            var directionList = directions.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.DirectionName }).ToList();
            return directionList;
        }
    }
}
