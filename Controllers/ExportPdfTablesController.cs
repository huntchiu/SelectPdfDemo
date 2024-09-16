using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace SelectPdfDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class ExportPdfTablesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Renderer = new HtmlToPdf();
        Renderer.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
        Renderer.Options.PdfPageSize = PdfPageSize.A4;
        Renderer.Options.MarginTop = 10;
        Renderer.Options.MarginBottom = 10;
        Renderer.Options.MarginLeft = 10;
        Renderer.Options.MarginRight = 10;



        string htmlContent = @"<html><head>
    <style>
        .header {
            background-color: #f0f0f0;
            font-weight: bold;
        }
    </style>
</head><body><table>  <thead>  <tr class='header'><th>Header 1</th><th>Header 2</th></tr>  </thead> <tbody>";
        for (int i = 1; i <= 100; i++)
        {
            htmlContent += $"<tr><td>Data {i}, 1</td><td>Data {i}, 2</td></tr>";
        }
        htmlContent += "</tbody></table></body></html>";



        var doc = Renderer.ConvertHtmlString(htmlContent);

        var memoryStream = new MemoryStream();
        await Task.Run(() => doc.Save(memoryStream));

        memoryStream.Position = 0;
        string fileName = "example.pdf";
        string contentType = "application/pdf";
        doc.Close();
        return File(memoryStream, contentType, fileName);
    }

}


