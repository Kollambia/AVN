using AVN.Automapper;
using AVN.Data.Migrations;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using AVN.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AVN.Controllers
{
    public class GradeBookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private GroupController groupController;

        public GradeBookController(IUnitOfWork unitOfWork, IMapper mapper, GroupController groupController)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.groupController = groupController;
        }

        public IActionResult Index()
        {
            //string username = User.Identity.Name;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.UserId = userId;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> StudentIndividualRecord(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new List<GradeBookVM>());

            var gradeBooks = (await unitOfWork.GradeBookRepository.GetAllAsync()).Where(x => x.StudentId == id);

            var mappedEntities = mapper.Map<GradeBook, GradeBookVM>(gradeBooks).ToList();
            return PartialView(mappedEntities ?? new List<GradeBookVM>());
        }

        [HttpGet]
        public async Task<ActionResult> GradeBookList(string groupId, int academicYearId, int subjectId, string userId)
        {
            if (string.IsNullOrEmpty(groupId))
                return PartialView(new List<GradeBookVM>());

            var gradeBooks = (await unitOfWork.GradeBookRepository.GetAllAsync()).Where(x => x.GroupId == groupId && 
                                                                                            x.AcademicYearId == academicYearId && 
                                                                                            x.SubjectId == subjectId);
            
            var mappedEntities = mapper.Map<GradeBook, GradeBookVM>(gradeBooks).ToList();
            return PartialView(mappedEntities ?? new List<GradeBookVM>());
        }

        [HttpPost]
        public async Task<ActionResult> GradeBookList(List<GradeBookVM> gradeBooks)
        {
            try
            {
                var mappedEntities = mapper.Map<GradeBookVM, GradeBook>(gradeBooks);
                foreach (var gradeBook in mappedEntities)
                {
                    try
                    {
                        await unitOfWork.GradeBookRepository.UpdateAsync(gradeBook);
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = $"Ошибка при сохранении изменений для студента {gradeBook.Student.FullName}. Ошибка: {ex}";
                        continue;
                    }
                }
                await unitOfWork.SaveChangesAsync();
                TempData["success"] = "Изменения успешно сохранены";
                return RedirectToAction("Index", "GradeBook");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex}";
                return RedirectToAction("Index", "GradeBook");
            }
        }

        public async Task<List<SelectListItem>> GetGroupsBySubjectAndAcademicYear(int academicYearId, int subjectId)
        {
            var groupIds = (await unitOfWork.GradeBookRepository.GetAllAsync()).Where(x => x.SubjectId == subjectId && x.AcademicYearId == academicYearId).Select(x => x.GroupId);
            return await groupController.GetGroupsByIds(groupIds.ToList());
        }
    }
}
