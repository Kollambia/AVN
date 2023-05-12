using AVN.Data.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers;

public class FacultyController : Controller
{
    private readonly UnitOfWork _unitOfWork;

    public FacultyController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFaculties()
    {
        var faculty = _unitOfWork.FacultyRepository.GetAll();
        return View();
    }

   
}