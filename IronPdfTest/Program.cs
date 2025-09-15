using IronPdfTest.FormFillers;
using IronPdfTest.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

License.LicenseKey = "IRONSUITE.FABIO.QUEIROZ.LMS.COM.1891-DD81795029-GZGAQA75RIRF3P-Y3EFZR5NHWIP-XUCFUOTLDAGT-BBO2GZHJGUMC-O54GUGY2DIER-OXQQNM557Q2Y-TLW56V-TLPKLMMHIO2QEA-DEPLOYMENT.TRIAL-63IXYB.TRIAL.EXPIRES.08.OCT.2025";

// static text pdf file
var pdf = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\TA10-3e-2013-specimen-extract.pdf");
var transactionTA10Json = File.ReadAllText(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Data\ta10.json");
var transactionFormData = JsonSerializer.Deserialize<JsonObject>(transactionTA10Json);

var mappingJson = File.ReadAllText(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Data\ta10Mapping.json");
var fieldMappings = JsonSerializer.Deserialize<Dictionary<string, PdfFieldMapping>>(mappingJson);

TA10PdfFormFiller.FillForm(pdf, transactionFormData!, fieldMappings!);

// editable pdf file
var editableTA10Pdf = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\TA10-editable-form-sample.pdf");
EditablePdfFormFiller.FillAndLockTA10Form(editableTA10Pdf);

var editablePdfSample = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\Sample-Fillable-PDF.pdf");
EditablePdfFormFiller.FillAndLockForm(editablePdfSample);

//var test1 = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\Manual-No-Match-Free-Text-Apr-25.pdf");
//var test2 = PdfDocument.FromFile(@"C:\Dev\Repos\IronPdfTest\IronPdfTest\Files\Manual-Referral-Check-Apr-25.pdf");

Console.WriteLine("Files saved in \"C:\\Dev\\Repos\\IronPdfTest\\IronPdfTest\\bin\\Debug\\net8.0\\");