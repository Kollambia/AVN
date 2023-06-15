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
            //string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");

            try
            {

                using (FileStream stream = new FileStream(pdfPath, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    Chunk chunk = new Chunk("Счет на оплату\n\n", FontFactory.GetFont("Times New Roman", 20, Font.BOLDITALIC));
                    pdfDoc.Add(new Paragraph(chunk));

                    // Создаем таблицу и задаем размер колонок
                    PdfPTable table = new PdfPTable(2);
                    table.TotalWidth = 500f;
                    table.LockedWidth = true;
                    float[] widths = new float[] { 200f, 300f };
                    table.SetWidths(widths);

                    BaseFont baseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\times.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font font = new Font(baseFont, 20, Font.NORMAL);

                    // Добавляем строки в таблицу
                    PdfPCell cell1 = new PdfPCell(new Phrase("Названия", font));
                    cell1.FixedHeight = 50;
                    cell1.BorderWidthTop = 0.1f;
                    cell1.BorderWidthBottom = 0.1f;
                    cell1.BorderWidthLeft = 0.1f;
                    cell1.BorderWidthRight = 0.1f;
                    cell1.PaddingLeft = 10;
                    table.AddCell(cell1);
                    table.AddCell(new Phrase("Данные", font));

                    PdfPCell cell2 = new PdfPCell(new Phrase("Факультет", font));
                    cell2.FixedHeight = 50;
                    cell2.BorderWidthTop = 0.1f;
                    cell2.BorderWidthBottom = 0.1f;
                    cell2.BorderWidthLeft = 0.1f;
                    cell2.BorderWidthRight = 0.1f;
                    cell2.PaddingLeft = 10;
                    table.AddCell(cell2);
                    table.AddCell(new Phrase(model.Faculty, font));

                    PdfPCell cell3 = new PdfPCell(new Phrase("Кафедра", font));
                    cell3.FixedHeight = 50;
                    cell3.BorderWidthTop = 0.1f;
                    cell3.BorderWidthBottom = 0.1f;
                    cell3.BorderWidthLeft = 0.1f;
                    cell3.BorderWidthRight = 0.1f;
                    cell3.PaddingLeft = 10;
                    table.AddCell(cell3);
                    table.AddCell(new Phrase(model.Department, font));

                    PdfPCell cell4 = new PdfPCell(new Phrase("Направление", font));
                    cell4.FixedHeight = 50;
                    cell4.BorderWidthTop = 0.1f;
                    cell4.BorderWidthBottom = 0.1f;
                    cell4.BorderWidthLeft = 0.1f;
                    cell4.BorderWidthRight = 0.1f;
                    cell4.PaddingLeft = 10;
                    table.AddCell(cell4);
                    table.AddCell(new Phrase(model.Direction, font));

                    PdfPCell cell5 = new PdfPCell(new Phrase("Группа", font));
                    cell5.FixedHeight = 50;
                    cell5.BorderWidthTop = 0.1f;
                    cell5.BorderWidthBottom = 0.1f;
                    cell5.BorderWidthLeft = 0.1f;
                    cell5.BorderWidthRight = 0.1f;
                    cell5.PaddingLeft = 10;
                    table.AddCell(cell5);
                    table.AddCell(new Phrase(model.Group, font));

                    PdfPCell cell6 = new PdfPCell(new Phrase("Форма обучения", font));
                    cell6.FixedHeight = 50;
                    cell6.BorderWidthTop = 0.1f;
                    cell6.BorderWidthBottom = 0.1f;
                    cell6.BorderWidthLeft = 0.1f;
                    cell6.BorderWidthRight = 0.1f;
                    cell6.PaddingLeft = 10;
                    table.AddCell(cell6);
                    table.AddCell(new Phrase(model.EducationForm, font));

                    PdfPCell cell7 = new PdfPCell(new Phrase("Имя студента", font));
                    cell7.FixedHeight = 50;
                    cell7.BorderWidthTop = 0.1f;
                    cell7.BorderWidthBottom = 0.1f;
                    cell7.BorderWidthLeft = 0.1f;
                    cell7.BorderWidthRight = 0.1f;
                    cell7.PaddingLeft = 10;
                    table.AddCell(cell7);
                    table.AddCell(new Phrase(model.FullName, font));

                    PdfPCell cell8 = new PdfPCell(new Phrase("Степень", font));
                    cell8.FixedHeight = 50;
                    cell8.BorderWidthTop = 0.1f;
                    cell8.BorderWidthBottom = 0.1f;
                    cell8.BorderWidthLeft = 0.1f;
                    cell8.BorderWidthRight = 0.1f;
                    cell8.PaddingLeft = 10;
                    table.AddCell(cell8);
                    table.AddCell(new Phrase(model.AcademicDegree, font));

                    PdfPCell cell9 = new PdfPCell(new Phrase("Номер счета на оплату", font));
                    cell9.FixedHeight = 50;
                    cell9.BorderWidthTop = 0.1f;
                    cell9.BorderWidthBottom = 0.1f;
                    cell9.BorderWidthLeft = 0.1f;
                    cell9.BorderWidthRight = 0.1f;
                    cell9.PaddingLeft = 10;
                    table.AddCell(cell9);
                    table.AddCell(new Phrase(model.PaymentAccountNumber, font));

                    PdfPCell cell10 = new PdfPCell(new Phrase("Сумма платежа", font));
                    cell10.FixedHeight = 50;
                    cell10.BorderWidthTop = 0.1f;
                    cell10.BorderWidthBottom = 0.1f;
                    cell10.BorderWidthLeft = 0.1f;
                    cell10.BorderWidthRight = 0.1f;
                    cell10.PaddingLeft = 10;
                    table.AddCell(cell10);
                    table.AddCell(new Phrase(model.PaymentAmount.ToString(), font));

                    PdfPCell cell11 = new PdfPCell(new Phrase("Назначение платежа", font));
                    cell11.FixedHeight = 50;
                    cell11.BorderWidthTop = 0.1f;
                    cell11.BorderWidthBottom = 0.1f;
                    cell11.BorderWidthLeft = 0.1f;
                    cell11.BorderWidthRight = 0.1f;
                    cell11.PaddingLeft = 10;
                    table.AddCell(cell11);
                    table.AddCell(new Phrase(model.PaymentPurpose, font));


                    pdfDoc.Add(table);

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
