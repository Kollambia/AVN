using AVN.Automapper;
using AVN.Common;
using AVN.Common.InheritedClasses;
using AVN.Common.PdfGenerator;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;
        private readonly UserManager<AppUser> userManager;

        public ReportController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult GetStudentsInGroup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetStudentsInGroup(int directionId, string groupId)
        {
            try
            {
                var students = unitOfWork.StudentRepository.GetAll();
                if (!string.IsNullOrEmpty(groupId))
                    students = students.Where(x => x.GroupId == groupId);
                else if (directionId > 0)
                    students = students.Where(x => x?.Group?.DirectionId == directionId);

                List<StudentsInGroup> studentsList = new List<StudentsInGroup>();
                foreach (var student in students)
                {
                    var model = new StudentsInGroup
                    {
                        FullName = $"{student.SName} {student.Name} {student.PName}",
                        Status = student.Status.GetDisplayName(),
                        DateOfBirth = student.DateOfBirth.ToShortDateString(),
                        EducationalLine = student.EducationalLine.GetDisplayName(),
                        GradeBookNumber = student.GradeBookNumber,
                        Gender = student.Gender.GetDisplayName(),
                        Citizenship = student.Citizenship.GetDisplayName(),
                        Address = student.Address,
                        PhoneNumber = student.PhoneNumber,
                        RecruitmentYear = student.RecruitmentYear.ToString(),
                        IsHasDebt = student.IsHasDebt ? "Есть" : "Нет",
                        Group = student?.Group?.GroupName,
                        Direction = student?.Group?.Direction?.DirectionName
                    };
                    studentsList.Add(model);
                }
              

                var reportGenerator = new ReportGenerator();
                var pdfInvoice = reportGenerator.GenerateStudentsInGroupPdf(studentsList);

                var fileBytes = System.IO.File.ReadAllBytes(pdfInvoice);

                var fileStreamResult = new FileStreamResult(new MemoryStream(fileBytes), "application/pdf");
                fileStreamResult.FileDownloadName = $"Студенты в группах.pdf";

                TempData["success"] = "Отчет успешно сформирован.";
                return PdfViewer(fileStreamResult);

            }
            catch (Exception ex)
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Home");
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
