using AVN.Automapper;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Web.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public SubjectController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            var subjects = await unitOfWork.SubjectRepository.GetAllAsync();
            var mappedSubjects = mapper.Map<Subject, SubjectVM>(subjects);
            return View(mappedSubjects);
        }

        public async Task<ActionResult> SubjectList(int facultyId, int departmentId, string employeeId, int courseId)
        {
            try
            {
                var subjects = await unitOfWork.SubjectRepository.GetAllAsync();
                if (!string.IsNullOrEmpty(employeeId))
                    subjects = subjects.Where(x => x.EmployeeId == employeeId);
                else if (departmentId > 0)
                    subjects = subjects.Where(x => x.DepartmentId == departmentId);
                else if (facultyId > 0)
                    subjects = subjects.Where(x => x.Department?.FacultyId == facultyId);
                if (courseId > 0)
                    subjects = subjects.Where(x => (int)x.Course == courseId);

                var mappedSubjects = mapper.Map<Subject, SubjectVM>(subjects).ToList();
                return PartialView(mappedSubjects);
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Subject");
            }
            
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }
            var mappedSubject = mapper.Map<Subject, SubjectVM>(subject);
            return View(mappedSubject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectVM subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedSubjects = mapper.Map<SubjectVM, Subject>(subject);
                    await unitOfWork.SubjectRepository.CreateAsync(mappedSubjects);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно добавлена";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Subject");
                }

            }

            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            var mappedSubject = mapper.Map<Subject, SubjectVM>(subject);

            return View(mappedSubject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubjectVM subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var mappedSubjects = mapper.Map<SubjectVM, Subject>(subject);
                    await unitOfWork.SubjectRepository.UpdateAsync(mappedSubjects);
                    await unitOfWork.SaveChangesAsync();

                    TempData["success"] = "Запись успешно изменена";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Subject");
                }
                
            }

            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }
            
            return View(subject);
        }

        // POST: Subject/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var subject = await context.Subjects
                    .Include(s => s.Schedule)
                    .Include(g => g.GradeBook)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (subject == null)
                {
                    TempData["error"] =
                        "Не удалось найти предмет. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Subject");
                }

                if (subject.Schedule.Any())
                {
                    await unitOfWork.ScheduleRepository.DeleteRangeAsync(subject.Schedule);
                }

                if (subject.GradeBook.Any())
                {
                    await unitOfWork.GradeBookRepository.DeleteRangeAsync(subject.GradeBook);
                }

                await unitOfWork.SubjectRepository.DeleteAsync(subject);
                await unitOfWork.SaveChangesAsync();

                TempData["success"] = "Запись успешно удалена";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Subject");
            }
            
        }

        public async Task<List<SelectListItem>> GetSubjects()
        {
            var subjects = await unitOfWork.SubjectRepository.GetAllAsync();
            var subjectsList = subjects.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Title }).ToList();
            return subjectsList;
        }

        public async Task<List<SelectListItem>> GetSubjectByEmployee(string employeeId)
        {
            var subjects = (await unitOfWork.SubjectRepository.GetAllAsync()).Where(x => x.EmployeeId == employeeId);
            var subjectsList = subjects.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Title }).ToList();
            return subjectsList;
        }

    }
}
