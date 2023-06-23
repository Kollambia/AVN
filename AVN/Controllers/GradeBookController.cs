using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AVN.Controllers
{
    public class GradeBookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GradeBookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            //string username = User.Identity.Name;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            return View();
        }
        public async Task<List<SelectListItem>> GetGroupsByGradeBook(int academicYearId, int subjectId)
        {
            var groupIds = (await unitOfWork.GradeBookRepository.GetAllAsync()).Where(x => x.SubjectId == subjectId && x.AcademicYearId == academicYearId).Select(x => x.GroupId);
            var entityList = (await unitOfWork.GroupRepository.GetAllAsync()).Where(x => groupIds.Contains(x.Id));
            return entityList.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.GroupName }).ToList();
        }
    }
}
