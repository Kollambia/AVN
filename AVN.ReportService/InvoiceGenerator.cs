using AVN.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace AVN.ReportService
{
    public class InvoiceGenerator
    {
        public void GeneratePdf(PaymentInvoiceModel model)
        {
            try
            {
                string pdfPath = Path.Combine(Environment.CurrentDirectory, "PaymentInvoice.pdf");

                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Add text to the document
                    Chunk chunk = new Chunk("Payment Invoice\n\n", FontFactory.GetFont("Arial", 20, Font.BOLDITALIC));
                    pdfDoc.Add(new Paragraph(chunk));

                    // Add payment information
                    string invoiceDetails = $"Faculty: {model.Faculty}\nDepartment: {model.Department}\nDirection: {model.Direction}\nGroup: {model.Group}\nCourse: {model.Course.GetCourseInWriting()}\nStudent: {model.FullName}\nDegree: {model.Degree}\nPayment Invoice Number: {model.InvoiceNumber}\nPayment Amount: {model.PaymentAmount}\nPayment Purpose: {model.PaymentPurpose}";
                    pdfDoc.Add(new Paragraph(invoiceDetails));

                    pdfDoc.Close();
                    stream.Close();
                }

                Console.WriteLine("PDF Created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
