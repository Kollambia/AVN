using AVN.Automapper;
using AVN.Common.Enums;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var groups = await unitOfWork.GroupRepository.GetAllAsync();
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupVM group)
        {
            if (ModelState.IsValid)
            {
                var mappedGroup = mapper.Map<GroupVM, Group>(group);
                await unitOfWork.GroupRepository.CreateAsync(mappedGroup);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            mappedGroup.FacultyId = group.Direction?.Department?.FacultyId;
            mappedGroup.DepartmentId = group.Direction?.DepartmentId;
            mappedGroup.DirectionId = group.DirectionId;
            //mappedGroup.DateCreate = group.DateCreate.Date;
            return View(mappedGroup);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GroupVM group)
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

        public async Task<List<SelectListItem>> GetGroupsByDirection(int directionId)
        {
            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => x.DirectionId == directionId);
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }
    }
}
