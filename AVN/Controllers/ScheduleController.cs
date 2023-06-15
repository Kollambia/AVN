using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
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
