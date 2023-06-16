using AVN.Models;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace AVN.PdfGenerator
{
    public class InvoiceGenerator
    {
        public string GeneratePdf(PaymentInvoiceModel model)
        {
            string pdfPath = Path.Combine(Environment.CurrentDirectory, "PaymentInvoice.pdf");
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.ttf");
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
                        .SetFontSize(20);
                    doc.Add(title);

                    Table table = new Table(UnitValue.CreatePointArray(new float[] { 200f, 300f }));
                    table.SetWidth(UnitValue.CreatePercentValue(100));

                    Cell cell1 = new Cell().Add(new Paragraph("Названия").SetFont(font)).SetHeight(50);
                    cell1.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell1);
                    table.AddCell(new Paragraph("Данные").SetFont(font));

                    Cell cell2 = new Cell().Add(new Paragraph("Факультет").SetFont(font)).SetHeight(50);
                    cell2.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell2);
                    table.AddCell(new Paragraph(model.Faculty).SetFont(font));

                    Cell cell3 = new Cell().Add(new Paragraph("Кафедра").SetFont(font)).SetHeight(50);
                    cell3.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell3);
                    table.AddCell(new Paragraph(model.Department).SetFont(font));

                    Cell cell4 = new Cell().Add(new Paragraph("Направление").SetFont(font)).SetHeight(50);
                    cell4.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell4);
                    table.AddCell(new Paragraph(model.Direction).SetFont(font));

                    Cell cell5 = new Cell().Add(new Paragraph("Группа").SetFont(font)).SetHeight(50);
                    cell5.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell5);
                    table.AddCell(new Paragraph(model.Group).SetFont(font));

                    Cell cell6 = new Cell().Add(new Paragraph("Форма обучения").SetFont(font)).SetHeight(50);
                    cell6.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell6);
                    table.AddCell(new Paragraph(model.EducationForm).SetFont(font));

                    Cell cell7 = new Cell().Add(new Paragraph("Имя студента").SetFont(font)).SetHeight(50);
                    cell7.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell7);
                    table.AddCell(new Paragraph(model.FullName).SetFont(font));

                    Cell cell8 = new Cell().Add(new Paragraph("Степень").SetFont(font)).SetHeight(50);
                    cell8.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell8);
                    table.AddCell(new Paragraph(model.AcademicDegree).SetFont(font));

                    Cell cell9 = new Cell().Add(new Paragraph("Номер счета на оплату").SetFont(font)).SetHeight(50);
                    cell9.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell9);
                    table.AddCell(new Paragraph(model.PaymentAccountNumber).SetFont(font));

                    Cell cell10 = new Cell().Add(new Paragraph("Сумма платежа").SetFont(font)).SetHeight(50);
                    cell10.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell10);
                    table.AddCell(new Paragraph(model.PaymentAmount.ToString()).SetFont(font));

                    Cell cell11 = new Cell().Add(new Paragraph("Назначение платежа").SetFont(font)).SetHeight(50);
                    cell11.SetBorder(Border.NO_BORDER);
                    table.AddCell(cell11);
                    table.AddCell(new Paragraph(model.PaymentPurpose).SetFont(font));

                    doc.Add(table);

                    doc.Close();
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