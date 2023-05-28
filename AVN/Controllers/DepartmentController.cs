﻿using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AVN.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            var mappedDepartment = mapper.Map<Department, DepartmentVM>(departments).ToList();
            return View(mappedDepartment);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentName,DepartmentShortName,FacultyId")] DepartmentVM department)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentVM,Department> (department);
                await unitOfWork.DepartmentRepository.CreateAsync(mappedDepartment);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var mappedDepartment = mapper.Map<Department, DepartmentVM> (department);
            return View(mappedDepartment);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,DepartmentShortName,FacultyId")] DepartmentVM department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentVM, Department> (department);
                await unitOfWork.DepartmentRepository.UpdateAsync(mappedDepartment);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            await unitOfWork.DepartmentRepository.DeleteAsync(department);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<List<SelectListItem>> GetDepartmentsByFaculty(int facultyId)
        {
            var departments = (await unitOfWork.DepartmentRepository.GetAllAsync()).Where(x => x.FacultyId == facultyId);
            var departmentList = departments.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.DepartmentName }).ToList();
            return departmentList;
        }
    }
}
