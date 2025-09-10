using IronPdfTest.FormFillers;
using IronPdfTest.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

License.LicenseKey = "IRONSUITE.FABIO.QUEIROZ.LMS.COM.1891-DD81795029-GZGAQA75RIRF3P-Y3EFZR5NHWIP-XUCFUOTLDAGT-BBO2GZHJGUMC-O54GUGY2DIER-OXQQNM557Q2Y-TLW56V-TLPKLMMHIO2QEA-DEPLOYMENT.TRIAL-63IXYB.TRIAL.EXPIRES.08.OCT.2025";

// fillable pdf file
var fillablePdf = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\Sample-Fillable-PDF.pdf");

EditablePdfFormFiller.FillForm(fillablePdf);

// static text pdf file
var pdf = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\TA10-3e-2013-specimen-extract.pdf");
var transactionTA10Json = File.ReadAllText(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Data\ta10.json");
var transactionFormData = JsonSerializer.Deserialize<JsonObject>(transactionTA10Json);

var mappingJson = File.ReadAllText(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Data\ta10Mapping.json");
var fieldMappings = JsonSerializer.Deserialize<Dictionary<string, PdfFieldMapping>>(mappingJson);

TA10PdfFormFiller.FillForm(pdf, transactionFormData!, fieldMappings!);

Console.WriteLine("Files saved in \"C:\\Dev\\Repos\\IronPdfTest\\IronPdfTest\\bin\\Debug\\net8.0\\");