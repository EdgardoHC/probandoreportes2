using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using probandoreportes2.Data;
using probandoreportes2.Models;

using iText.Layout;

using System.IO;
using System.Linq;
using iText.Layout.Borders;

namespace probandoreportes2.Controllers

{
    public class ReportController : Controller
    {
        private readonly Data.ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult GeneratePdf()
        {
            var employees = _context.Employees.ToList();
            byte[] pdf = GeneratePdfReport(employees);

            return File(pdf, "application/pdf", "EmployeesReport.pdf");
        }

        private byte[] GeneratePdfReport(List<Employees> employees)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Listado de empleados"));
                var table = new Table(4, true);
                //  table.SetBorder(Border.NO_BORDER);
                table.SetBorder(new SolidBorder(1f));
                table.AddHeaderCell(CreateCell("ID", true));
                table.AddHeaderCell(CreateCell("Nombres",true));
                table.AddHeaderCell(CreateCell("Apellidos",true));
                table.AddHeaderCell(CreateCell("Direccion",true));

                foreach (var employee in employees)
                {
                    table.AddCell(employee.EmployeeID.ToString());
                    table.AddCell(employee.FirstName);
                    table.AddCell(employee.LastName);
                    table.AddCell(employee.Address);
                }

                document.Add(table);
                document.Close();

                return stream.ToArray();
            }
        }
        // Método para crear celdas con bordes
        private Cell CreateCell(string content, bool isHeader)
        {
            var cell = new Cell().Add(new Paragraph(content));

            if (isHeader)
            {
                cell.SetBold();
            }
            else
            {
                cell.SetBorder(Border.NO_BORDER);
            }

            cell.SetBorderBottom(new SolidBorder(1));
            cell.SetBorderTop(new SolidBorder(1));
            cell.SetBorderLeft(new SolidBorder(1));
            cell.SetBorderRight(new SolidBorder(1));

            return cell;
        }
    }
}
