using AVN.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace AVN.ReportService
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
                    Chunk chunk = new Chunk("Payment Invoice\n\n", FontFactory.GetFont("Arial", 20, Font.BOLDITALIC));
                    pdfDoc.Add(new Paragraph(chunk));

                    // Add payment information
                    string invoiceDetails = $"Faculty: {model.Faculty}\nDepartment: {model.Department}\nDirection: {model.Direction}\nGroup: {model.Group}\nCourse: {model.Course}\nStudent: {model.FullName}\nDegree: {model.AcademicDegree}\nPayment Invoice Number: {model.PaymentAccountNumber}\nPayment Amount: {model.PaymentAmount}\nPayment Purpose: {model.PaymentPurpose}";
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
