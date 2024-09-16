using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace SelectPdfDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class ExportPdfController : ControllerBase
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

        string htmlContent = "<h1>Hello, World!</h1><p>This is a simple PDF generated from HTML content.</p >";

        var doc = Renderer.ConvertHtmlString(htmlContent);

        // 创建MemoryStream并保存PDF文档
        var memoryStream = new MemoryStream();
        await Task.Run(() => doc.Save(memoryStream));

        // 重置流位置，以便读取完整内容
        memoryStream.Position = 0;

        string fileName = "example.pdf";
        string contentType = "application/pdf";

        // 关闭PDF文档，但不要立即释放MemoryStream
        doc.Close();

        // 返回MemoryStream作为文件下载
        return File(memoryStream, contentType, fileName);
    }

}


