using AVN.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace AVN.PdfGenerator
{
    public class InvoiceGenerator
    {
        public string GeneratePdf(PaymentInvoiceModel model)
        {
            string pdfPath = Path.Combine(Environment.CurrentDirectory, "PaymentInvoice.pdf");
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");

            try
            {

                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font font = new Font(baseFont, 12, Font.NORMAL);

                    Chunk chunk = new Chunk("Счет на оплату\n\n", font);
                    pdfDoc.Add(new Paragraph(chunk));

                    string invoiceDetails = $"Факультет: {model.Faculty}\n" +
                                            $"Кафедра: {model.Department}\n" +
                                            $"Направление: {model.Direction}\n" +
                                            $"Группа: {model.Group}\n" +
                                            $"Курс: {model.Course}\n" +
                                            $"Имя студента: {model.FullName}\n" +
                                            $"Степень: {model.AcademicDegree}\n" +
                                            $"Номер счета на оплату: {model.PaymentAccountNumber}\n" +
                                            $"Сумма платежа: {model.PaymentAmount}\n" +
                                            $"Назначение платежа: {model.PaymentPurpose}";

                    pdfDoc.Add(new Paragraph(invoiceDetails, font));

                    pdfDoc.Close();
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex}");
            }

            return pdfPath;
        }
    }
}
