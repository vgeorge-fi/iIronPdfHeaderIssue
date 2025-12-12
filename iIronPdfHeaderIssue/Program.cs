using Google.Protobuf.WellKnownTypes;
using IronPdf;
using IronPdf.Rendering;
using System.ComponentModel;
using System.Reflection.Metadata;

Installation.EnableWebSecurity = true;
IronPdf.License.LicenseKey = @"";

var options = new ChromePdfRenderOptions
{
    MarginTop = 50,
    MarginBottom = 10,
    MarginLeft = 10,
    MarginRight = 10,
    PaperSize = PdfPaperSize.A4,
    CssMediaType = PdfCssMediaType.Print,
    PaperOrientation = PdfPaperOrientation.Portrait,
    FitToPaperMode = IronPdf.Engines.Chrome.FitToPaperModes.AutomaticFit,
};

var renderer = new ChromePdfRenderer { RenderingOptions = options };
var page = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "page.html"));
var content = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "content.html"));

var document = renderer.RenderHtmlAsPdf(page.Replace("@body:content", content));

var header = new HtmlHeaderFooter
{
    LoadStylesAndCSSFromMainHtmlDocument = true,
    HtmlFragment = content,
    DrawDividerLine = false,
};

document.AddHtmlHeaders(header, options.MarginLeft, options.MarginRight, 10);

var output = Path.Combine(AppContext.BaseDirectory, "output.pdf");

document.SaveAs(output);

Console.WriteLine($"PDF Generated: {output}");