﻿using AVN.Data.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers;

public class FacultyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public FacultyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFaculties()
    {
        var faculty = await _unitOfWork.FacultyRepository.GetAll();
        return View();
    }

   
}