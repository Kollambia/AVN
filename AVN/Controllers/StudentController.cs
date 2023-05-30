using AVN.Areas.Identity.Pages.Account;
using AVN.Automapper;
using AVN.Business;
using AVN.Common.Enums;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.PdfGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AVN.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Student
        [HttpGet]
        public IActionResult Index()
        {
            StudentViaFilterVM studentViaFilterVM = new StudentViaFilterVM
            {
                studentVMs = new List<StudentVM>()
            };
            return View(studentViaFilterVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(StudentViaFilterVM filter)
        {
            var students = await unitOfWork.StudentRepository.GetAllAsync();
            if (filter.GroupId.HasValue)
                students = students.Where(x => x.GroupId == filter.GroupId);
            else if (filter.DirectionId.HasValue)
                students = students.Where(x => x.Group.DirectionId == filter.DirectionId);
            else if (filter.DepartmentId.HasValue)
                students = students.Where(x => x.Group.Direction.DepartmentId == filter.DepartmentId);
            else if (filter.FacultyId.HasValue)
                students = students.Where(x => x.Group.Direction.Department.FacultyId == filter.FacultyId);


            StudentViaFilterVM filteredStudents = new()
            {
                FacultyId= filter?.FacultyId,
                DepartmentId= filter?.DepartmentId,
                DirectionId= filter?.DirectionId,
                GroupId= filter?.GroupId,
                studentVMs = mapper.Map<Student, StudentVM>(students).ToList()
            };

            return View(filteredStudents);
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
            var studentOrderService = new OrderService(context);
            Student student = new Student();
            var updatedStudent = studentOrderService.SetStudentStatusAndGradeBookNumber(student);
            var mappedStudent = mapper.Map<Student, StudentVM>(updatedStudent);
            return View(mappedStudent);
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVM student)
        {

            if (ModelState.IsValid)
            {
                var mappedStudent = mapper.Map<StudentVM, Student>(student);
                await unitOfWork.StudentRepository.CreateAsync(mappedStudent);
                await unitOfWork.SaveChangesAsync();
                CreateStudentUser(student);
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
            var mappedStudent = mapper.Map<Student, StudentVM>(student);
            mappedStudent.FacultyId = student.Group.Direction.Department.FacultyId;
            mappedStudent.DepartmentId = student.Group.Direction.DepartmentId;
            mappedStudent.DirectionId = student.Group.DirectionId;
            return View(mappedStudent);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentVM student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedStudent = mapper.Map<StudentVM, Student>(student);
                await unitOfWork.StudentRepository.UpdateAsync(mappedStudent);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await unitOfWork.StudentRepository.GetByIdAsync(id);
            await unitOfWork.StudentRepository.DeleteAsync(student);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GeneratePaymentInvoice(string id)
        {

            var studentTask = unitOfWork.StudentRepository.GetByIdAsync(id);
            var groupTask = unitOfWork.GroupRepository.GetByIdAsync((int)studentTask.Result.GroupId);
            var directionTask = unitOfWork.DirectionRepository.GetByIdAsync((int)groupTask.Result.DirectionId);
            var departmentTask = unitOfWork.DepartmentRepository.GetByIdAsync((int)directionTask.Result.DepartmentId);
            var facultyTask = unitOfWork.FacultyRepository.GetByIdAsync((int)departmentTask.Result.FacultyId);

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
                FullName = student.SName,
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

        public void CreateStudentUser(StudentVM studentVm)
        {
            var studentUser = MapStudentUser(studentVm);
            context.AppUsers.Add(studentUser);
            context.SaveChanges();
        }
        public AppUser MapStudentUser(StudentVM student)
        {
            return new AppUser()
            {
                Id = "1",
                SName = student.SName,
                Name = student.Name,
                PName = student.PName,
                DateOfBirth = student.DateOfBirth,
                StudingForm = student.StudingForm,
                EducationalLine = student.EducationalLine,
                AcademicDegree = student.AcademicDegree,
                GradeBookNumber = student.GradeBookNumber,
                Status = student.Status,
                Gender = student.Gender,
                Citizenship = student.Citizenship,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                FacultyId = student.FacultyId,
                DepartmentId = student.DepartmentId,
                DirectionId = student.DirectionId,
                GroupId = student.GroupId

            };
        }
    }
}
