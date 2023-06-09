﻿using AVN.Automapper;
using AVN.Business;
using AVN.Common.Enums;
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

        public async Task<ActionResult> StudentList(int facultyId, int departmentId, int directionId, string groupId, int groupType, string fullname)
        {
            try
            {
                var students = await unitOfWork.StudentRepository.GetAllAsync();
                if (!string.IsNullOrEmpty(fullname))
                {
                    var studentOrderService = new OrderService(context);
                    students = studentOrderService.GetStudentsByFullName(fullname);
                    return PartialView(students.Select(s => mapper.Map<Student, StudentVM>(s)) ?? new List<StudentVM>());
                }
                else if (!string.IsNullOrEmpty(groupId))
                {
                    students = students.Where(x => x.GroupId == groupId);
                }
                else if (directionId > 0)
                {
                    students = students.Where(x => x.Group?.DirectionId == directionId);
                }
                else if (departmentId > 0)
                {
                    students = students.Where(x => x.Group?.Direction?.DepartmentId == departmentId);
                }
                else if (facultyId > 0)
                {
                    students = students.Where(x => x.Group?.Direction?.Department?.FacultyId == facultyId);
                }
                else
                {
                    return PartialView(new List<StudentVM>());
                }

                if (groupType > 0)
                {
                    students = students.Where(x => (int)x?.Group?.GroupType == groupType);
                }

                var mappedStudents = students.Select(s => mapper.Map<Student, StudentVM>(s)).ToList();
                return PartialView(mappedStudents ?? new List<StudentVM>());
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
            
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
            try
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
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
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

        public IActionResult RefreshTables()
        {
            return RedirectToAction("Index", "Order");
        }

        public async Task<IActionResult> Details(string id)
        {
            var student = await context.Students
                .Include(s => s.Group)
                .ThenInclude(g => g.Direction)
                .ThenInclude(d => d.Department)
                .ThenInclude(f => f.Faculty)
                .FirstOrDefaultAsync(s => s.Id == id);

            var studentPayment = await context.StudentPayments
                .FirstOrDefaultAsync(p => p.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            var studentVM = new StudentVM
            {
                Id = student.Id,
                SName = student.SName,
                Name = student.Name,
                PName = student.PName,
                Status = student.Status,
                DateOfBirth = student.DateOfBirth,
                EducationalLine = student.EducationalLine,
                GradeBookNumber = student.GradeBookNumber,
                Gender = student.Gender,
                Citizenship = student.Citizenship,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                RecruitmentYear = student.RecruitmentYear,
                Faculty = student.Group.Direction.Department.Faculty,
                Department = student.Group.Direction.Department,
                Direction = student.Group.Direction,
                Group = student.Group
            };
            var mappedStudentPayment = mapper.Map<StudentPayment, StudentPaymentVM>(studentPayment);

            var studentAndPaymentVM = new StudentAndStudentPaymentVM
            {
                Student = studentVM,
                StudentPayment = mappedStudentPayment
            };

            return View(studentAndPaymentVM);
        }

        public async Task<IActionResult> StudentMovementDetails(int id)
        {
            var entity = await unitOfWork.StudentMovementRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }
            var mappedEntity = mapper.Map<StudentMovement, StudentMovementVM>(entity);
            return View(mappedEntity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                // to do переделать этот кошмар
                var studentOrderService = new OrderService(context);
                var updatedStudent = studentOrderService.SetStudentStatusAndGradeBookNumber();
                var mappedStudent = mapper.Map<Student, StudentVM>(updatedStudent);
                mappedStudent.Login = mappedStudent.Password = mappedStudent.ConfirmPassword = mappedStudent.GradeBookNumber;
                return View(mappedStudent);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
        }

        public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
        {
            public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
            {
                // Bypass password validation by returning a successful result without performing any validation
                return Task.FromResult(IdentityResult.Success);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVM student)
        {
            if (ModelState.IsValid)
            {
                var mappedStudent = mapper.Map<StudentVM, Student>(student);

                var newId = Guid.NewGuid().ToString();
                mappedStudent.Id = newId;
                
                var user = new AppUser() { UserName = student.GradeBookNumber, Id = newId, FullName = student.FullName};

                userManager.PasswordValidators.Clear();
                userManager.PasswordValidators.Add(new CustomPasswordValidator<AppUser>());

                try
                {
                    var result = await userManager.CreateAsync(user, student.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, RoleConst.StudentRole);
                        await unitOfWork.StudentRepository.CreateAsync(mappedStudent);
                        await unitOfWork.SaveChangesAsync();
                        TempData["success"] = "Абитуриент успешно создан";
                        return RedirectToAction("Enrolled", "Student");
                    }
                    else if (!result.Succeeded)
                    {
                        TempData["error"] =  $"Ошибка при создании учетной записи абитуриента: {result.Errors.First().Description}";
                        return View(student);
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return View(student);
                }
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var student = await context.Students
                   .Include(s => s.Group)
                   .ThenInclude(g => g.Direction)
                   .ThenInclude(d => d.Department)
                   .FirstOrDefaultAsync(s => s.Id == id);

                if (student == null)
                {
                    TempData["error"] = "Не удалось найти студента. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Student");
                }

                var mappedStudent = mapper.Map<Student, StudentEditVM>(student);
                return View(mappedStudent);
            }
            catch(Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentEditVM student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedStudent = mapper.Map<StudentEditVM, Student>(student);

                    await unitOfWork.StudentRepository.UpdateAsync(mappedStudent);
                    await unitOfWork.SaveChangesAsync();

                    var group = await context.Groups.FirstOrDefaultAsync(s => s.Id == mappedStudent.GroupId);
                    if (group == null)
                        return RedirectToAction(nameof(Index));

                    TempData["success"] = "Изменения успешно сохранены";
                    var returnetView = group.GroupType == GroupType.Students ? "Index" : group.GroupType.ToString();
                    return RedirectToAction($"{returnetView}");
                }
                return View(student);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
        }

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var student = await context.Students
                   .Include(g => g.StudentMovements)
                   .Include(d => d.Orders)
                   .Include(d => d.Orders)
                   .Include(s => s.StudentPayments)
                   .FirstOrDefaultAsync(s => s.Id == id);

                if (student == null)
                {
                    TempData["error"] = "Не удалось найти студента. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Student");
                }

                if (student.StudentMovements.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите переводы студентов связанные со студентом";
                    return RedirectToAction("Index", "Student");
                }
                if (student.Orders.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите приказы связанные со студентом";
                    return RedirectToAction("Index", "Student");
                }

                if (student.StudentPayments.Any())
                {
                    TempData["error"] = "Не удалось удалить запись. Удалите платежи связанные со студентом";
                    return RedirectToAction("Index", "Student");
                }
                
                await unitOfWork.StudentRepository.DeleteAsync(student);

                // Удаляем пользователя
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (!result.Succeeded)
                    {
                        // Обрабатываем ошибки, возникшие в процессе удаления пользователя
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }

                // Сохраняем все изменения
                await unitOfWork.SaveChangesAsync();

                TempData["success"] = "Запись успешно удалена";

                var studentGroupType = student?.Group?.GroupType;
                if (studentGroupType == null)
                    return RedirectToAction(nameof(Index));

                var returnedView = studentGroupType == GroupType.Students ? "Index" : studentGroupType.ToString();
                return RedirectToAction($"{returnedView}");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
        }


        public async Task<IActionResult> StudentMovementEdit(int id)
        {
            try
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
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentMovementEdit(int id, StudentMovementVM studentMovement)
        {
            if (id != studentMovement.Id)
            {
                return NotFound();
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var mapped = mapper.Map<StudentMovementVM, StudentMovement>(studentMovement);
                    await unitOfWork.StudentMovementRepository.UpdateAsync(mapped);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction("Edit", "Student", new { id = mapped.StudentId });
                }
                return View(studentMovement);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return View(studentMovement);
            }
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

        public async Task<IActionResult> DeleteStudentMovementConfirmed(int id)
        {
            try
            {
                var student = await unitOfWork.StudentMovementRepository.GetByIdAsync(id);
                if (student == null)
                {
                    TempData["error"] = "Не найдена текущая запись";
                    return RedirectToAction("Index", "Student");
                }
                await unitOfWork.StudentMovementRepository.DeleteAsync(student);
                await unitOfWork.SaveChangesAsync();

                TempData["success"] = "Запись успешно удалена";
                return RedirectToAction("Edit", "Student", new { id = student.StudentId });
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }

        }

        public IActionResult GeneratePaymentInvoice(int paymentId, string studentId)
        {
            try
            {
                var student = unitOfWork.StudentRepository.GetById(studentId);
                var studentPayment = unitOfWork.StudentPaymentRepository.GetById(paymentId);

                var studentOrderService = new OrderService(context);
                var result = studentOrderService.CreateStudentPaymentDetail(studentPayment);

                if (result.Success)
                {
                    var fileBytes = System.IO.File.ReadAllBytes(result.Data);

                    var fileStreamResult = new FileStreamResult(new MemoryStream(fileBytes), "application/pdf");
                    fileStreamResult.FileDownloadName = $"Платежный счёт {student.FullName}.pdf";

                    TempData["success"] = result.Message;
                    return PdfViewer(fileStreamResult);
                }
                else
                {
                    TempData["error"] = $"{result.Message}  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                }

                return RedirectToAction("Index", "StudentPayment", new { id = studentId });
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Student");
            }
        }

        public IActionResult PdfViewer(FileStreamResult fileStreamResult)
        {
            var fileName = fileStreamResult.FileDownloadName;
            var encodedFileName = Uri.EscapeDataString(fileName);

            var contentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("inline");
            contentDisposition.FileName = encodedFileName;

            Response.Headers["Content-Disposition"] = contentDisposition.ToString();

            // Return the file stream with the appropriate content type
            return File(fileStreamResult.FileStream, fileStreamResult.ContentType);
        }

    }
}
