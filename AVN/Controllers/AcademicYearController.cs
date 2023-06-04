using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Controllers
{
    public class AcademicYearController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AcademicYearController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public async Task<List<SelectListItem>> GetAcademicYears()
        {
            var academicYears = (await unitOfWork.AcademicYearRepository.GetAllAsync()).OrderByDescending(x => x.Id);
            //var academicYears = await unitOfWork.AcademicYearRepository.GetAllAsync();
            var academYearList = academicYears.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
            return academYearList;
        }
    }
}
