using AVN.Automapper;
using AVN.Common.Enums;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using iText.Kernel.Pdf.Colorspace;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AVN.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public GroupController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Group
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GroupList(int facultyId, int departmentId, int academYearId)
        {
            var groups = await unitOfWork.GroupRepository.GetAllAsync();
            if (departmentId > 0)
                groups = groups.Where(x => x.Direction.DepartmentId == departmentId);
            else if (facultyId > 0)
                groups = groups.Where(x => x.Direction.Department.FacultyId == facultyId);
            if (academYearId > 0)
                groups = groups.Where(x => x.AcademicYearId == academYearId);

            var mappedGroups = mapper.Map<Group, GroupVM>(groups).ToList();
            return PartialView(mappedGroups);
        }

        // GET: Group/Details/5
        public async Task<IActionResult> Details(string id)
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
                var newId = Guid.NewGuid().ToString();
                var mappedGroup = mapper.Map<GroupVM, Group>(group);
                mappedGroup.Id = newId;
                await unitOfWork.GroupRepository.CreateAsync(mappedGroup);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Group/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var group = await context.Groups
                .Include(g => g.AcademicYear)
                .Include(g => g.Direction)
                .ThenInclude(d => d.Department)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }
            var mappedGroup = mapper.Map<Group, GroupVM>(group);
            mappedGroup.FacultyId = group.Direction?.Department?.FacultyId;
            mappedGroup.DepartmentId = group.Direction?.DepartmentId;
            mappedGroup.DirectionId = group.DirectionId;
            mappedGroup.DateCreate = group.DateCreate.Date;
            return View(mappedGroup);
        }

        // POST: Group/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, GroupVM group)
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
        public async Task<IActionResult> Delete(string id)
        {
            var group = await unitOfWork.GroupRepository.GetByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Group/Delete/5
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var group = await context.Groups
                    .Include(s => s.Students)
                    .Include(o => o.Orders)
                    .Include(g => g.GroupEmployees)
                    .Include(s => s.Schedule)
                    .Include(g => g.GradeBook)
                    .FirstOrDefaultAsync(i => i.Id == id);
                if (group == null)
                {
                    TempData["error"] =
                        "Не удалось найти группу. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Group");
                }

                TempData["success"] = "Запись успешно удалена";
                await unitOfWork.StudentRepository.DeleteRangeAsync(group.Students);
                await unitOfWork.OrderRepository.DeleteRangeAsync(group.Orders);
                context.GroupEmployees.RemoveRange(group.GroupEmployees);
                await unitOfWork.ScheduleRepository.DeleteRangeAsync(group.Schedule);
                await unitOfWork.GradeBookRepository.DeleteRangeAsync(group.GradeBook);


                await unitOfWork.GroupRepository.DeleteAsync(group);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Group");
            }
            
        }
        public async Task<List<SelectListItem>> GetGroups()
        {
            var groups = await unitOfWork.GroupRepository.GetAllAsync();
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }

        public async Task<List<SelectListItem>> GetGroupsByDirection(int directionId, int groupType)
        {
            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => x.DirectionId == directionId && (int)x.GroupType == groupType);
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }

        public async Task<List<SelectListItem>> GetGroupsByIds(List<string> groupIds)
        {
            var entityList = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => groupIds.Contains(x.Id));
            return entityList.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
        }

        public async Task<List<SelectListItem>> GetEnrolledGroupsByDirection(int directionId)
        {
            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => x.DirectionId == directionId).Where(x => x.GroupType == GroupType.Enrolled);
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }

        public async Task<List<SelectListItem>> GetGroupsToExport(int facultyId, int movementTypeId, int academicYearId)
        {
            if (facultyId == 0 && academicYearId == 0 && movementTypeId == 0)
                return new List<SelectListItem>();

            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => 
                    x.Direction.Department.FacultyId == facultyId && x.AcademicYearId == academicYearId);

            var movement = await unitOfWork.MovementTypeRepository.GetByIdAsync(movementTypeId);
            if (movement != null)
            {
                switch (movement.MoveType)
                {
                    case MoveType.Enlisted: //Зачисление
                        groups = groups.Where(x => x.GroupType == GroupType.Enrolled); //c абитуриентов
                        break;

                    case MoveType.Translated: //Перевод
                        groups = groups.Where(x => x.GroupType == GroupType.Students); // с студентов
                        break;


                    case MoveType.Graduated: //Окончание
                        groups = groups.Where(x => x.GroupType == GroupType.Graduated);
                        break;

                    default:
                        // Handle any other move types here, if needed
                        break;
                }
            }
            
            // to do доделать
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }

        public async Task<List<SelectListItem>> GetGroupsToImport(int facultyId, int movementTypeId, int academicYearId)
        {

            if (facultyId == 0 || academicYearId == 0 || movementTypeId == 0)
                return new List<SelectListItem>();

            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x =>
                    x.Direction.Department.FacultyId == facultyId && x.AcademicYearId == academicYearId);

            var movement = await context.MovementTypes.FirstOrDefaultAsync(m => m.Id == movementTypeId);
            if (movement is null)
            {
                throw new Exception("Тип перемещения не найден");
            }

            switch (movement.MoveType)
            {
                case MoveType.Enlisted: //Зачисление
                    groups = groups.Where(x => x.GroupType == GroupType.Students); // к студентам
                    break;

                case MoveType.Translated: //Перевод
                    groups = groups.Where(x => x.GroupType == GroupType.Students); // к студентов
                    break;


                case MoveType.Graduated: //Окончание
                    groups = groups.Where(x => x.GroupType == GroupType.Graduated);
                    break;

                default:
                    // Handle any other move types here, if needed
                    break;
            }
            // to do доделать
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }
    }
}
