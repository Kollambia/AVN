using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers
{
    public class GradeBookController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GradeBookController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
