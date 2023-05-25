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
            try
            {

                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Add text to the document
                    Chunk chunk = new Chunk("Счет на оплату\n\n", FontFactory.GetFont("Arial", 20, Font.BOLDITALIC));
                    pdfDoc.Add(new Paragraph(chunk));

                    // Add payment information
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

                    pdfDoc.Add(new Paragraph(invoiceDetails));

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
