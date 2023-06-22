using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using AVN.Common.Customs;

namespace AVN.Common.PdfGenerator
{
    public class InvoiceGenerator
    {
        public string GenerateStudentPaymentPdf(PaymentInvoice model)
        {
            string pdfPath = Path.Combine(Environment.CurrentDirectory, "PaymentInvoice.pdf");
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");
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
    }
}
