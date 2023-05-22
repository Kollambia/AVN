using AVN.Data;
using AVN.Model.Entities;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace AVN.Business
{
    public class StudentPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IViewRenderService _viewRenderService;
        private readonly IPdfService _pdfService;

        public StudentPaymentService(AppDbContext context, IViewRenderService viewRenderService, IPdfService pdfService)
        {
            _context = context;
            _viewRenderService = viewRenderService;
            _pdfService = pdfService;
        }

        public async Task<StudentPaymentDetail> CreatePaymentInvoice(int studentId)
        {
            // 6.1 Получаем студента из базы данных
            var student = await _context.Students.Include(s => s.Group.Direction.Department.Faculty)
                .SingleOrDefaultAsync(s => s.Id == studentId);

            // 6.2 Создаем новый платежный счет
            var paymentInvoice = new StudentPaymentDetail
            {
                StudentPaymentId = studentId,
                Payment = student.StudentPayments.First(sp => sp.AcademicYear == DateTime.Now.Year).Contract / 2,
                SpecialPurpose = "Оплата за учебу",
                Number = GenerateInvoiceNumber()
            };

            // 6.3 Создаем модель данных для отображения
            var model = new PaymentInvoiceViewModel
            {
                FacultyName = student.Group.Direction.Department.Faculty.FacultyName,
                DepartmentName = student.Group.Direction.Department.DepartmentName,
                DirectionName = student.Group.Direction.DirectionName,
                GroupName = student.Group.GroupName,
                CourseName = student.Group.Course.ToString(),
                FullName = student.FullName,
                FormOfEducation = student.StudingForm.ToString(),
                AcademicDegree = student.AcademicDegree.ToString(),
                InvoiceNumber = paymentInvoice.Number,
                PaymentAmount = paymentInvoice.Payment,
                SpecialPurpose = paymentInvoice.SpecialPurpose
            };

            // 6.4 Получаем HTML-разметку счета
            var invoiceHtml = await _viewRenderService.RenderViewAsString("Invoice", model);

            // 7. Преобразуем HTML-разметку в PDF
            var pdfData = _pdfService.ConvertHtmlToPdf(invoiceHtml);

            // 9. Сохраняем данные счета в базе данных
            _context.StudentPaymentDetails.Add(paymentInvoice);
            await _context.SaveChangesAsync();

            // Возвращаем объект счета
            return paymentInvoice;
        }

        private string GenerateInvoiceNumber()
        {
            // Сгенерируем уникальный номер счета. В этом примере мы просто используем GUID.
            return Guid.NewGuid().ToString();
        }
    }

    public interface IViewRenderService
    {
        Task<string> RenderViewToStringAsync(string viewName, object model);
    }

    public class ViewRenderService : IViewRenderService
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView("~/Views", viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(actionContext, viewResult.View, viewDictionary, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), sw, new HtmlHelperOptions());

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }
    }

    public interface IPdfService
    {
        byte[] ConvertHtmlToPdf(string html);
    }

    public class PdfService : IPdfService
    {
        private IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] ConvertHtmlToPdf(string html)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);
            return file;
        }
    }


}
