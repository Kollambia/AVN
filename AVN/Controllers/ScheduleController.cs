using AVN.Automapper;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Schedule/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(context.Groups, "Id", "GroupName");
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Title");
            ViewData["EmployeeId"] = new SelectList(context.Employees, "Id", "Name");

            var model = new ScheduleVM();
            model.Schedules = new List<Schedule> { new Schedule(), new Schedule() };
            return View(model);
        }

        // POST: Schedule/Create
        // POST: Schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleVM scheduleList)
        {
            if (ModelState.IsValid)
            {
                foreach (var schedule in scheduleList.Schedules)
                {
                    context.Add(schedule);
                }
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Update ViewData for Group, Subject, Teacher dropdown lists if model state is invalid.
            ViewData["GroupId"] = new SelectList(context.Groups, "Id", "GroupName");
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Title");
            ViewData["EmployeeId"] = new SelectList(context.Employees, "Id", "Name");
            return View(scheduleList);
        }


        public async Task<ActionResult> ScheduleList(int facultyId, int departmentId, int directionId, string groupId, int groupType)
        {
            var schedule = await unitOfWork.ScheduleRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(groupId))
            {
                schedule = schedule.Where(x => x.GroupId == groupId);
            }
            else if (directionId > 0)
            {
                schedule.Where(x => x.Group.DirectionId == directionId);
            }
            else if (departmentId > 0)
            {
                schedule.Where(x => x.Group.Direction.DepartmentId == departmentId);
            }
            else if (facultyId > 0)
            {
                schedule = schedule.Where(x => x.Group.Direction.Department.FacultyId == facultyId);
            }
            else
            {
                return PartialView(new List<ScheduleVM>());
            }

            if (groupType > 0)
            {
                schedule = schedule.Where(x => (int)x.Group.GroupType == groupType);
            }

            var mappedSchedule = schedule.Select(s => mapper.Map<Schedule, ScheduleVM>(s)).ToList();
            return PartialView(mappedSchedule ?? new List<ScheduleVM>());
        }
        
    }
}
