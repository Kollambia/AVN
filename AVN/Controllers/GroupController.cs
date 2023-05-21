using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public GroupController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Group
        public async Task<IActionResult> Index()
        {
            var groups = await unitOfWork.GroupRepository.GetAllAsync("Direction");
            var mappedGroups = mapper.Map<Group, GroupVM>(groups);
            return View(mappedGroups);
        }

        // GET: Group/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var group = await unitOfWork.GroupRepository.GetByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Group/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Directions = await unitOfWork.DirectionRepository.GetAllAsync();
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupName,Course,DirectionId")] GroupVM group)
        {
            if (ModelState.IsValid)
            {
                var mappedGroup = mapper.Map<GroupVM, Group>(group);
                mappedGroup.DateCreate= DateTime.Now;
                await unitOfWork.GroupRepository.CreateAsync(mappedGroup);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Directions = await unitOfWork.DirectionRepository.GetAllAsync();
            return View(group);
        }

        // GET: Group/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var group = await unitOfWork.GroupRepository.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            var mappedGroup = mapper.Map<Group, GroupVM>(group);
            ViewBag.Directions = await unitOfWork.DirectionRepository.GetAllAsync();
            return View(mappedGroup);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,Course,DirectionId")] GroupVM group)
        {
            if (id != group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedGroup = mapper.Map<GroupVM, Group>(group);
                await unitOfWork.GroupRepository.UpdateAsync(mappedGroup);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Directions = await unitOfWork.DirectionRepository.GetAllAsync();
            return View(group);
        }

        // GET: Group/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var group = await unitOfWork.GroupRepository.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Group/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await unitOfWork.GroupRepository.GetByIdAsync(id);
            await unitOfWork.GroupRepository.DeleteAsync(group);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
