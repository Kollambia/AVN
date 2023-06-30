using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using AVN.Common.Customs;
using AVN.Common.InheritedClasses;
using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.IO.Font;
using System.Linq;
using System.Text.RegularExpressions;

namespace AVN.Common.PdfGenerator
{
    public class ReportGenerator
    {
        public string GenerateStudentPaymentPdf(PaymentInvoice model)
        {
            string pdfPath = System.IO.Path.Combine(Environment.CurrentDirectory, "PaymentInvoice.pdf");
            string fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");
            try
            {
                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdfDoc = new PdfDocument(writer);
                    Document doc = new Document(pdfDoc);

                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);

                    Paragraph title = new Paragraph("Счет на оплату")
                    .SetFont(font)
                    .SetBold()
                    .SetItalic()
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER); // Align title to the center
                    doc.Add(title);

                    // Add each line of data
                    AddLine(doc, model.Faculty, 16, ColorConstants.RED, font);
                    AddLine(doc, model.Department, 18, ColorConstants.BLACK, font);
                    AddLine(doc, $"Группа: {model.Group}", 18, ColorConstants.BLUE, font);
                    AddLine(doc, model.FullName, 22, ColorConstants.BLACK, font);
                    AddLine(doc, $"Форма обучения: {model.EducationForm} {model.AcademicDegree}", 12, ColorConstants.BLACK, font);
                    AddLine(doc, $"Номер счета на оплату: {model.PaymentAccountNumber}", 12, ColorConstants.BLACK, font);
                    AddLine(doc, $"Сумма платежа: {model.PaymentAmount}", 12, ColorConstants.BLACK, font);
                    AddLine(doc, $"Назначение платежа: {model.PaymentPurpose}", 12, ColorConstants.BLACK, font);
                    Thread.Sleep(10);
                    doc.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //todo переделать по человекчески чтоб пользователю было понятно
                //throw new Exception($"Ошибка: {ex}");
            }

            return pdfPath;
        }

        private static void AddLine(Document doc, string value, float fontSize, Color color, PdfFont font)
        {
            Paragraph line = new Paragraph(value)
                .SetFont(font)
                .SetFontSize(fontSize)
                .SetFontColor(color)
                .SetHeight(30)
                .SetTextAlignment(TextAlignment.CENTER);
            doc.Add(line);
        }

        public string GenerateStudentsInGroupPdf(List<StudentsInGroup> students)
        {
            string pdfPath = System.IO.Path.Combine(Environment.CurrentDirectory, "PaymentInvoice.pdf");
            string fontPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");

            try
            {
                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    PdfWriter writer = new PdfWriter(stream);
                    PdfDocument pdfDoc = new PdfDocument(writer);
                    Document doc = new Document(pdfDoc, PageSize.A4, false);

                    // Load the font with Cyrillic support
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                    writer.SetCloseStream(false);

                    var groupedStudents = students
                        .GroupBy(s => s.Direction)
                        .Select(group1 => new
                        {
                            Direction = group1.Key,
                            Group = group1.GroupBy(s => s.Group)
                        })
                        .ToList();

                    int totalFirstLevelGroups = groupedStudents.Count;
                    int currentFirstLevelGroupIndex = 0;

                    foreach (var group1 in groupedStudents)
                    {
                        currentFirstLevelGroupIndex++;

                        Paragraph directionNameHeader = new Paragraph($"Специализация: {group1.Direction}")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(20)
                            .SetFont(font);

                        doc.Add(directionNameHeader);

                        int totalSecondLevelGroups = group1.Group.Count();
                        int currentSecondLevelGroupIndex = 0;

                        foreach (var group2 in group1.Group)
                        {
                            currentSecondLevelGroupIndex++;

                            Paragraph groupNameHeader = new Paragraph($"Группа: {group2.Key}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(16)
                                .SetFont(font);

                            doc.Add(groupNameHeader);

                            Paragraph subheader = new Paragraph("Студенты")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(15)
                            .SetFont(font);

                            doc.Add(subheader);

                            //doc.Add(new Paragraph(""));

                            //LineSeparator ls = new LineSeparator(new SolidLine());
                            //doc.Add(ls);

                            doc.Add(new Paragraph(""));

  
                            doc.Add(GetPdfTable(group2.ToList(), font));

     
                            int n = pdfDoc.GetNumberOfPages();
                            for (int i = 1; i <= n; i++)
                            {
                                Paragraph pageFooter = new Paragraph(string.Format("Страница {0}", i))
                                    .SetTextAlignment(TextAlignment.RIGHT)
                                    .SetFontSize(10)
                                    .SetFont(font);

                                doc.ShowTextAligned(pageFooter, 559, 20, i, TextAlignment.RIGHT, VerticalAlignment.BOTTOM, 0);
                            }
                            if (currentSecondLevelGroupIndex < totalSecondLevelGroups)
                            {
                                doc.Add(new AreaBreak());
                            }
                        }

                        if (currentFirstLevelGroupIndex < totalFirstLevelGroups)
                        {
                            doc.Add(new AreaBreak());
                        }
                    }


                    doc.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                throw;
            }

            return pdfPath;
        }

        private Table GetPdfTable(List<StudentsInGroup> students, PdfFont font)
        {
            // Table
            Table table = new Table(7, false);

            // Headings
            Cell cellFullName = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("ФИО").SetFont(font));

            Cell cellEducationalLine = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Образование").SetFont(font));

            Cell cellGradeBookNumber = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Зачётка").SetFont(font));

            Cell cellGender = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Пол").SetFont(font));

            Cell cellCitizenship = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Гражданство").SetFont(font));

            Cell cellPhoneNumber = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Телефон").SetFont(font));

            Cell cellDebt = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Долг").SetFont(font));

            table.AddCell(cellFullName);
            table.AddCell(cellEducationalLine);
            table.AddCell(cellGradeBookNumber);
            table.AddCell(cellGender);
            table.AddCell(cellCitizenship);
            table.AddCell(cellPhoneNumber);
            table.AddCell(cellDebt);


            foreach (var item in students)
            {
                Cell fullName = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.FullName).SetFont(font));

                Cell educationalLine = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.EducationalLine).SetFont(font));

                Cell gradeBookNumber = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.GradeBookNumber).SetFont(font));

                Cell gender = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.Gender).SetFont(font));

                Cell citizenship = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.Citizenship).SetFont(font));

                Cell phoneNumber = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.PhoneNumber).SetFont(font));

                Cell isHasDebt = new Cell(1, 1)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item.IsHasDebt).SetFont(font));

                table.AddCell(fullName);
                table.AddCell(educationalLine);
                table.AddCell(gradeBookNumber);
                table.AddCell(gender);
                table.AddCell(citizenship);
                table.AddCell(phoneNumber);
                table.AddCell(isHasDebt);
            }

            return table;
        }


        // Custom page event handler to add the group name in the header
        public class GroupHeaderHandler : IEventHandler
        {
            private PdfFont font;

            public string GroupName { get; set; }

            public GroupHeaderHandler(PdfFont font)
            {
                this.font = font;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();

                // Create a canvas for the header
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

                // Set the font and font size for the header
                canvas.SetFontAndSize(font, 12);

                // Add the group name in the header
                canvas.BeginText()
                    .MoveText(36, page.GetPageSize().GetTop() - 36)
                    .ShowText("Group: " + GroupName.ToUpper())
                    .EndText();

                // Release the canvas
                canvas.Release();
            }
        }
    }
}
