﻿using AVN.Automapper;
using AVN.Common.Enums;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.PdfGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AVN.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Student
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(StudentViaFilterVM filter)
        {
            //var student = await unitOfWork.StudentRepository.GetByIdAsync(id);

            //if (student == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await unitOfWork.StudentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Status,DateOfBirth,StudingForm," +
                                                      "EducationalLine,AcademicDegree,GradeBookNumber," +
                                                      "Gender,Citizenship,Address,PhoneNumber,Orders,GroupId")] 
                                                        Student student)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.StudentRepository.CreateAsync(student);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Status,DateOfBirth,StudingForm,EducationalLine,AcademicDegree,GradeBookNumber,Gender,Citizenship,Address,PhoneNumber,Orders,GroupId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await unitOfWork.StudentRepository.UpdateAsync(student);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await unitOfWork.StudentRepository.GetByIdAsync(id);
            await unitOfWork.StudentRepository.DeleteAsync(student);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GeneratePaymentInvoice(int id)
        {

            var studentTask = unitOfWork.StudentRepository.GetByIdAsync(id);
            var groupTask = unitOfWork.GroupRepository.GetByIdAsync(studentTask.Result.GroupId);
            var directionTask = unitOfWork.DirectionRepository.GetByIdAsync(groupTask.Result.DirectionId);
            var departmentTask = unitOfWork.DepartmentRepository.GetByIdAsync(directionTask.Result.DepartmentId);
            var facultyTask = unitOfWork.FacultyRepository.GetByIdAsync(departmentTask.Result.FacultyId);

            await Task.WhenAll(studentTask, groupTask, directionTask, departmentTask, facultyTask);

            var student = await studentTask;
            var group = await groupTask;
            var direction = await directionTask;
            var department = await departmentTask;
            var faculty = await facultyTask;

            var contract = await unitOfWork.StudentPaymentRepository.FindByConditionAsync(s => s.StudentId == id);

            var latestContract = contract.OrderByDescending(c => c.AcademicYear).FirstOrDefault();

            var model = new PaymentInvoiceModel
            {
                Faculty = faculty.FacultyName,
                Department = department.DepartmentName ?? "Нет данных",
                Direction = direction.DirectionName ?? "Нет данных",
                Group = group.GroupName ?? "Нет данных",
                Course = student.Group?.Course.GetCourseInWriting() ?? "Нет данных",
                FullName = student.FullName,
                EducationForm = student.StudingForm.ToString(),
                AcademicDegree = student.AcademicDegree.ToString(),
                PaymentAccountNumber = GeneratePaymentAccountNumber(),
                PaymentAmount = 40000,
                PaymentPurpose = "Оплата за обучение"   
            };

            var invoiceGenerator = new InvoiceGenerator();
            var pdfInvoice = invoiceGenerator.GeneratePdf(model);


            //когда будет контракт расскоментить

            //var paymentDetail = new StudentPaymentDetail
            //{
            //    StudentPaymentId = 1,
            //    Payment = model.PaymentAmount,
            //    Number = model.PaymentAccountNumber,
            //    SpecialPurpose = model.PaymentPurpose
            //};

            //var createdPaymentDetail = await unitOfWork.StudentPaymentDetailRepository.CreateAsync(paymentDetail);

            //await unitOfWork.SaveChangesAsync();

            byte[] fileBytes = System.IO.File.ReadAllBytes(pdfInvoice);
            return File(fileBytes, "application/pdf","Contract.pdf");
        }

        private static string GeneratePaymentAccountNumber()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
