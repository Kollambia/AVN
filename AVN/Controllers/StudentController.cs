using AVN.Automapper;
using AVN.Business;
using AVN.Common.Enums;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.PdfGenerator;
using AVN.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AVN.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;
        private static List<TransferStudentVM> _transferExportStudents = new(); //временное рещение
        private static List<TransferStudentVM> _transferImportStudents = new(); //временное решение
        private static string _exportGroupId; //временное решение
        private static string _importGroupId; //временное решение
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }

        // GET: Student
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Archived()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Graduated()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Expelled()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult AcademicLeaved()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Enrolled()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ActionName(string[] selectedItems)
        {
            // Process the selected items here
            // You can perform database operations, update the model, etc.

            return Json(new { success = true }); // Return a JSON response if needed
        }

        public async Task<ActionResult> StudentList(int facultyId = 0, int departmentId = 0, int directionId = 0, string groupId = null, int groupType = 0)
        {
            var students = await unitOfWork.StudentRepository.GetAllAsync();
            if (groupId != null)
                students = students.Where(x => x.GroupId == groupId);
            else if (directionId > 0)
                students = students.Where(x => x.Group.DirectionId == directionId);
            else if (departmentId > 0)
                students = students.Where(x => x.Group.Direction.DepartmentId == departmentId);
            else if (facultyId > 0)
                students = students.Where(x => x.Group.Direction.Department.FacultyId == facultyId);

            var mappedStudents = mapper.Map<Student, StudentVM>(students);
            if (groupType > 0)
                mappedStudents = mappedStudents.Where(x => (int)x.Group.GroupType == groupType).ToList();
            else if (facultyId == 0 && departmentId == 0 && directionId == 0 && groupId == null && groupType == 0)
                return PartialView(new List<StudentVM>());

            return PartialView(mappedStudents);
        }

        [HttpGet]
        public async Task<ActionResult> ExportStudentList(string groupId)
        {
            var students = await unitOfWork.StudentRepository.GetAllAsync();
            if (groupId != null)
                students = students.Where(x => x.GroupId == groupId);
            else
                return PartialView("ExportStudentList", new List<TransferStudentVM>());

            var mappedStudents = mapper.Map<Student, TransferStudentVM>(students);
            if (_exportGroupId != groupId)
            {
                _exportGroupId = groupId;
                _transferExportStudents.Clear();
                _transferImportStudents.Clear();
            }
            if (_transferExportStudents.Count() == 0 && _transferImportStudents.Count() == 0) 
                _transferExportStudents = mappedStudents.ToList();

            return PartialView("ExportStudentList", _transferExportStudents);
        }

        [HttpPost]
        public async Task<ActionResult> ExportStudentList(List<TransferStudentVM> transferStudents) // to do make sync method
        {
            var idsToRemove = new List<string>();
            foreach (var student in transferStudents)
            {
                var studentGroup = await unitOfWork.GroupRepository.GetByIdAsync(student.GroupId);
                student.Group = studentGroup;
                if (student.Selected == true)
                {
                    student.Transfered = true;
                    student.Selected = false;
                    idsToRemove.Add(student.Id);
                    _transferImportStudents.Add(student);
                }
            }
            _transferExportStudents.RemoveAll(exStudent => idsToRemove.Contains(exStudent.Id));
            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public async Task<ActionResult> ImportStudentList(string groupId)
        {
            var students = await unitOfWork.StudentRepository.GetAllAsync();
            if (groupId != null)
                students = students.Where(x => x.GroupId == groupId);
            else
                return PartialView("ImportStudentList", new List<TransferStudentVM>());

            var mappedStudents = mapper.Map<Student, TransferStudentVM>(students);
            if (_importGroupId != groupId)
            {
                _importGroupId = groupId;
                _transferExportStudents.Clear();
                _transferImportStudents.Clear();
            }
            if (_transferImportStudents.Count == 0)
                _transferImportStudents = mappedStudents.ToList();

            return PartialView("ImportStudentList", _transferImportStudents);
        }

        [HttpPost]
        public async Task<ActionResult> RevertTransferStudents(List<TransferStudentVM> transferStudents)
        {
            var idsToRemove = new List<string>();
            foreach (var student in transferStudents)
            {
                var studentGroup = await unitOfWork.GroupRepository.GetByIdAsync(student.GroupId);
                student.Group = studentGroup;
                if (student.Selected == true)
                {
                    student.Transfered = false;
                    student.Selected = false;
                    idsToRemove.Add(student.Id);
                    _transferExportStudents.Add(student);
                }
            }
            _transferImportStudents.RemoveAll(exStudent => idsToRemove.Contains(exStudent.Id));
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public async Task<ActionResult> SaveImportedStudents(List<TransferStudentVM> transferStudents)
        {
            var studentOrderService = new OrderService(context);
            foreach (var student in transferStudents)
            {
                if (student.Transfered == true)
                {
                    var changedStudent = await unitOfWork.StudentRepository.GetByIdAsync(student.Id);
                    changedStudent.GroupId = _importGroupId;
                    changedStudent.Status = studentOrderService.GetStudentStatusByGroupId(_importGroupId);
                    await unitOfWork.StudentRepository.UpdateAsync(changedStudent);
                    await unitOfWork.SaveChangesAsync();
                    _transferExportStudents.Clear();
                    _transferImportStudents.Clear();
                }

            }
            return RedirectToAction("Index", "Order");
        }

        public IActionResult CancelImportStudents()
        {
            bool isNeedToCancel = _transferImportStudents.Any(x => x.Transfered);
            if (isNeedToCancel)
            {
                _transferExportStudents.Clear();
                _transferImportStudents.Clear();
            }
            return RedirectToAction("Index", "Order");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
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
            // to do переделать этот кошмар
            // оставляй как есть)
            var studentOrderService = new OrderService(context);
            Student student = new Student();
            var updatedStudent = studentOrderService.SetStudentStatusAndGradeBookNumber(student);
            var mappedStudent = mapper.Map<Student, StudentVM>(updatedStudent);
            mappedStudent.Login = mappedStudent.GradeBookNumber;
            mappedStudent.RecruitmentYear = DateTime.Now.Year;
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

                var newId = Guid.NewGuid().ToString();
                mappedStudent.Id = newId;
                
                var user = new AppUser() { UserName = student.Login, Id = newId };
                var result = await userManager.CreateAsync(user, student.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, RoleConst.StudentRole);

                    await unitOfWork.StudentRepository.CreateAsync(mappedStudent);
                    await unitOfWork.SaveChangesAsync();
                    return RedirectToAction("Enrolled", "Student");
                }
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var student = await context.Students
                .Include(s => s.Group)
                .ThenInclude(g => g.Direction)
                .ThenInclude(d => d.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var mappedStudent = mapper.Map<Student, StudentVM>(student);
            mappedStudent.FacultyId = student?.Group?.Direction.Department.FacultyId;
            mappedStudent.DepartmentId = student?.Group?.Direction.DepartmentId;
            mappedStudent.DirectionId = student?.Group?.DirectionId;
            return View(mappedStudent);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, StudentVM student)
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
                var groupTask = unitOfWork.GroupRepository.GetByIdAsync(studentTask.Result.GroupId);
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
                //EducationForm = student.StudingForm.ToString(),
                //AcademicDegree = student.AcademicDegree.ToString(),
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
