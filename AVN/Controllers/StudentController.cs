using AVN.Automapper;
using AVN.Business;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
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
        public static List<TransferStudentVM> TransferImportStudents
        {
            get { return _transferImportStudents; }
        }
        public static List<TransferStudentVM> TransferExportStudents
        {
            get { return _transferExportStudents; }
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


        public async Task<ActionResult> StudentList(int facultyId, int departmentId, int directionId, string groupId, int groupType)
        {
            var students = await unitOfWork.StudentRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(groupId))
            {
                students = students.Where(x => x.GroupId == groupId);
            }
            else if (directionId > 0)
            {
                students = students.Where(x => x.Group.DirectionId == directionId);
            }
            else if (departmentId > 0)
            {
                students = students.Where(x => x.Group.Direction.DepartmentId == departmentId);
            }
            else if (facultyId > 0)
            {
                students = students.Where(x => x.Group.Direction.Department.FacultyId == facultyId);
            }
            else
            {
                return PartialView(new List<StudentVM>());
            }

            if (groupType > 0)
            {
                students = students.Where(x => (int)x.Group.GroupType == groupType);
            }

            var mappedStudents = students.Select(s => mapper.Map<Student, StudentVM>(s)).ToList();
            return PartialView(mappedStudents ?? new List<StudentVM>());
        }

        public async Task<ActionResult> StudentMovementList(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                return PartialView(new List<StudentMovementVM>());

            var studentMovements = (await unitOfWork.StudentMovementRepository.GetAllAsync()).Where(x => x.StudentId == studentId);

            var mappedList = mapper.Map<StudentMovement, StudentMovementVM>(studentMovements);

            if (!mappedList.Any())
                return PartialView(new List<StudentMovementVM>());

            return PartialView(mappedList);
        }

        [HttpGet]
        public IActionResult ExportStudentList(string groupId)
        {
            var students = unitOfWork.StudentRepository.GetAll();
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
            if (_transferExportStudents.Count() == 0 && (_transferImportStudents.Count() == 0 
                || _transferImportStudents.All(x => x.Transfered == false))) 
                _transferExportStudents = mappedStudents.ToList();

            return PartialView("ExportStudentList", _transferExportStudents);
        }

        [HttpPost]
        public IActionResult ExportStudentList(List<TransferStudentVM> transferStudents) 
        {
            var idsToRemove = new List<string>();
            foreach (var student in transferStudents)
            {
                var studentGroup = unitOfWork.GroupRepository.GetById(student.GroupId);
                student.Group = studentGroup;
                if (student.Selected == true)
                {
                    student.Transfered = true;
                    student.Selected = false;
                    if (!_transferImportStudents.Select(x => x.Id).Contains(student.Id))
                    {
                        idsToRemove.Add(student.Id);
                        _transferImportStudents.Add(student);
                    }
                }
            }
            _transferExportStudents.RemoveAll(exStudent => idsToRemove.Contains(exStudent.Id));
            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public IActionResult ImportStudentList(string groupId)
        {
            var students = unitOfWork.StudentRepository.GetAll();
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
        public IActionResult RevertTransferStudents(List<TransferStudentVM> transferStudents)
        {
            var idsToRemove = new List<string>();
            foreach (var student in transferStudents)
            {
                var studentGroup = unitOfWork.GroupRepository.GetById(student.GroupId);
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

        public List<TransferStudentVM> GetImportedStudents()
        {
            return _transferImportStudents;
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

        public async Task<IActionResult> StudentMovementEdit(int id)
        {
            var studentMovement = await context.StudentMovements
                .Include(s => s.AcademicYear)
                .Include(g => g.MovementType)
                .Include(d => d.NewGroup)
                .Include(k => k.OldGroup)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (studentMovement == null)
            {
                return NotFound();
            }

            var mappedList = mapper.Map<StudentMovement, StudentMovementVM>(studentMovement);

            return View(mappedList);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentMovementEdit(int id, StudentMovementVM studentMovement)
        {
            if (id != studentMovement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mapped = mapper.Map<StudentMovementVM, StudentMovement>(studentMovement);
                await unitOfWork.StudentMovementRepository.UpdateAsync(mapped);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction("Edit", "Student", new { id = mapped.StudentId });
            }
            return View(studentMovement);
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

        public async Task<IActionResult> StudentMovementDelete(int id)
        {
            var student = await unitOfWork.StudentMovementRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var mapped = mapper.Map<StudentMovement, StudentMovementVM>(student);
            return View(mapped);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudentMovementConfirmed(string id)
        {
            var student = await unitOfWork.StudentMovementRepository.GetByIdAsync(id);
            await unitOfWork.StudentMovementRepository.DeleteAsync(student);
            await unitOfWork.SaveChangesAsync();
            return RedirectToAction("Edit", "Student", new { id = student.StudentId });
        }

        public IActionResult GeneratePaymentInvoice(int paymentId, string studentId)
        {
            var studentPayment = unitOfWork.StudentPaymentRepository.GetById(paymentId);

            var studentOrderService = new OrderService(context);
            var fileBytes = studentOrderService.CreateStudentPaymentDetail(studentPayment);
            if (fileBytes != null)
            {
                return File(fileBytes, "application/pdf", "Contract.pdf");
            }
            return RedirectToAction("Index", "StudentPayment", new { id = studentId });
        }

    }
}
