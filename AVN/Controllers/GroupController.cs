using AVN.Automapper;
using AVN.Common.Enums;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
                groups = groups.Where(x => x.Direction?.DepartmentId == departmentId);
            else if (facultyId > 0)
                groups = groups.Where(x => x.Direction?.Department?.FacultyId == facultyId);
            if (academYearId > 0)
                groups = groups.Where(x => x.AcademicYearId == academYearId);

            var mappedGroups = mapper.Map<Group, GroupVM>(groups).ToList();
            return PartialView(mappedGroups);
        }

        // GET: Group/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var group = await context.Groups
                .Include(d => d.AcademicYear)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (group == null)
            {
                return NotFound();
            }
            var mappedGroup = mapper.Map<Group, GroupVM>(group);
            return View(mappedGroup);
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
            try
            {
                if (ModelState.IsValid)
                {
                    var newId = Guid.NewGuid().ToString();
                    var mappedGroup = mapper.Map<GroupVM, Group>(group);
                    mappedGroup.Id = newId;
                    await unitOfWork.GroupRepository.CreateAsync(mappedGroup);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно добавлена";
                    return RedirectToAction(nameof(Index));
                }

                return View(group);
            }
            catch(Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Group");
            }

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
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedGroup = mapper.Map<GroupVM, Group>(group);
                    await unitOfWork.GroupRepository.UpdateAsync(mappedGroup);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction(nameof(Index));
                }
                return View(group);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Group");
            }
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

                if (group.Students.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите студентов связанные с группой";
                    return RedirectToAction("Index", "Group");
                }

                if (group.Orders.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите приказы связанные с группой";
                    return RedirectToAction("Index", "Group");
                }

                if (group.GroupEmployees.Any())
                {
                    TempData["error"] = "Не удалось удалить запись.";
                    return RedirectToAction("Index", "Group");
                }

                if (group.Schedule.Any())
                {
                    await unitOfWork.ScheduleRepository.DeleteRangeAsync(group.Schedule);
                    //TempData["error"] = "Не удалось удалить запись. Удалите расписания связанные с группой";
                    //return RedirectToAction("Index", "Group");
                }

                if (group.GradeBook.Any())
                {
                    await unitOfWork.GradeBookRepository.DeleteRangeAsync(group.GradeBook);
                    //TempData["error"] = "Не удалось удалить запись. Удалите ведомость связанную с группой";
                    //return RedirectToAction("Index", "Group");
                }

                await unitOfWork.GroupRepository.DeleteAsync(group);
                await unitOfWork.SaveChangesAsync();

                TempData["success"] = "Запись успешно удалена";
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

        public async Task<List<SelectListItem>> GetGroupsByDirection(int directionId, int groupType = 0)
        {
            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => x.DirectionId == directionId);
            if (groupType > 0)
                groups = groups.Where(x => (int)x.GroupType == groupType);

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
                    x.Direction?.Department?.FacultyId == facultyId && x.AcademicYearId == academicYearId);

            var movement = await unitOfWork.MovementTypeRepository.GetByIdAsync(movementTypeId);
            if (movement != null)
            {
                switch (movement.MoveType)
                {
                    case MoveType.Translated: //Перевод
                    case MoveType.Graduated: //Окончание вуза
                    case MoveType.Expelled: //Отчисление с вуза
                    case MoveType.AcademicLeaved: //Академический отпуск
                        groups = groups.Where(x => x.GroupType == GroupType.Students); //активных студентов
                        break;
                    case MoveType.Enlisted: //Зачисление
                        groups = groups.Where(x => x.GroupType == GroupType.Enrolled); //абитуриентов
                        break;
                    case MoveType.NextCourseTransfer: //Перевод на следующий курс
                        groups = groups.Where(x => x.GroupType == GroupType.Students); //активных студентов
                        break;
                    case MoveType.RestoredAcademic: //Восстановление из академ
                        groups = groups.Where(x => x.GroupType == GroupType.AcademicLeaved); //академ отпуск
                        break;
                    case MoveType.RestoredExpelled: //Восстановление из отчисл
                        groups = groups.Where(x => x.GroupType == GroupType.Expelled); //отчисленные
                        break;
                    case MoveType.RestoredGraduared: //Восстановление из законч
                        groups = groups.Where(x => x.GroupType == GroupType.Graduated); //окончившиеся
                        break;
                    case MoveType.InArchive: //Восстановление из архива
                        groups = groups.Where(x => x.GroupType == GroupType.Archived); //архивированные
                        break;
                    default:
                        groups = groups.Where(x => x.GroupType == GroupType.Archived); //архивированные
                        break;
                }
            }
            
            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }

        public async Task<List<SelectListItem>> GetGroupsToImport(int facultyId, int movementTypeId, int academicYearId)
        {

            if (facultyId == 0 || academicYearId == 0 || movementTypeId == 0)
                return new List<SelectListItem>();

            var groups = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x =>
                    x.Direction?.Department?.FacultyId == facultyId && x.AcademicYearId == academicYearId);

            var movement = await context.MovementTypes.FirstOrDefaultAsync(m => m.Id == movementTypeId);
            if (movement is null)
            {
                throw new Exception("Тип перемещения не найден");
            }

            switch (movement.MoveType)
            {
                case MoveType.Translated: // Перевод
                case MoveType.Enlisted: // Зачисление
                case MoveType.NextCourseTransfer: // Перевод на следующий курс
                case MoveType.RestoredAcademic: // Восстановление из академ
                case MoveType.RestoredExpelled: // Восстановление из отчисл
                case MoveType.RestoredGraduared: // Восстановление из законч
                case MoveType.InArchive: // Восстановление из архива
                    groups = groups.Where(x => x.GroupType == GroupType.Students); // к активным студентам
                    break;
                case MoveType.Graduated: // Окончание вуза
                    groups = groups.Where(x => x.GroupType == GroupType.Graduated); // к окончившим
                    break;
                case MoveType.Expelled: // Отчисление с вуза
                    groups = groups.Where(x => x.GroupType == GroupType.Expelled); // к отчисленным
                    break;
                case MoveType.AcademicLeaved: // Академический отпуск
                    groups = groups.Where(x => x.GroupType == GroupType.AcademicLeaved); // к академ отпуску
                    break;
                default:
                    groups = groups.Where(x => x.GroupType == GroupType.Archived); // к архивным студентам
                    break;
            }

            var groupList = groups.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
            return groupList;
        }
    }
}
