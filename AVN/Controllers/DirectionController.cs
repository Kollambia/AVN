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
    public class DirectionController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public DirectionController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Direction
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DirectionList(int facultyId)
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
            try
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
            catch(Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Direction");
            }
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
            try
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
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Direction");
            }
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
            try
            {
                var direction = await context.Directions
                    .Include(g => g.Groups)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (direction == null)
                {
                    TempData["error"] =
                        "Не удалось найти специализацию. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Direction");
                }

                TempData["success"] = "Запись успешно удалена";
                await unitOfWork.GroupRepository.DeleteRangeAsync(direction.Groups);

                await unitOfWork.DirectionRepository.DeleteAsync(direction);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Direction");
            }
            
        }

        public async Task<List<SelectListItem>> GetDirectionsByDepartment(int departmentId)
        {
            var directions = (await unitOfWork.DirectionRepository.GetAllAsync()).Where(x => x.DepartmentId == departmentId);
            var directionList = directions.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.DirectionName }).ToList();
            return directionList;
        }
    }
}
