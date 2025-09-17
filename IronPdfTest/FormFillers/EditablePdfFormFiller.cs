using IronSoftware.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace IronPdfTest.FormFillers;

internal class EditablePdfFormFiller
{
    internal static byte[] FillAndLockForm(PdfDocument pdf)
    {
        var test = pdf.Form;

        foreach (var field in pdf.Form)
        {
            var name = field.Name;
            var value = field.FormType;
        }

        pdf.Form.SingleOrDefault(x => x.Name == "Name")!.Value = "TEST NAME";
        pdf.Form.SingleOrDefault(x => x.Name == "Name of Dependent")!.Value = "Full Test Name";
        pdf.Form.SingleOrDefault(x => x.Name == "Age\t of Dependent")!.Value = "45";
        pdf.Form.SingleOrDefault(x => x.Name == "Dropdown2")!.Value = "Choice 4";
        pdf.Form.SingleOrDefault(x => x.Name == "Option 1")!.Value = "On";
        pdf.Form.SingleOrDefault(x => x.Name == "Option 3")!.Value = "On";

        MemoryStream pdfAsStream = pdf.Stream;
        byte[] pdfAsByte = pdf.BinaryData;

        pdfAsStream.Position = 0;
        var pdfReloaded = new PdfDocument(pdfAsByte);

        pdfReloaded.Flatten();

        // for testing purposes, save the locked PDF to disk
        pdfReloaded.SaveAs("Sample_Filled.pdf");

        return pdfReloaded.BinaryData;
    }

    internal static byte[] FillAndLockTA10Form(PdfDocument pdf)
    {
        pdf.Form.SingleOrDefault(x => x.FullName == "PropertyPack.Address.FullAddress")!.Value = "29A Lushington Road \nLondon";

        FillPostcode(pdf, "NW10 5UX");

        MemoryStream pdfAsStream = pdf.Stream;
        byte[] pdfAsByte = pdf.BinaryData;

        pdfAsStream.Position = 0;
        var pdfReloaded = new PdfDocument(pdfAsByte);

        pdfReloaded.Flatten();

        // for testing purposes, save the locked PDF to disk
        pdfReloaded.SaveAs("TA10_Filled_Base_Test.pdf");

        return pdfReloaded.BinaryData;
    }

    //private static void FillPostcode(PdfDocument pdf, string postcode, int fontSize = 16)
    //{
    //    var chars = postcode.Trim().ToUpper().ToCharArray();

    //    var postcodeFields = pdf.Form
    //        .Where(f => Regex.IsMatch(f.Name, @"^Postcode\s+\d+$"))
    //        .Select(f => new
    //        {
    //            Field = f,
    //            Index = int.Parse(Regex.Match(f.Name, @"\d+").Value)
    //        })
    //        .OrderBy(x => x.Index)
    //        .ToList();

    //    for (int i = 0; i < postcodeFields.Count && i < chars.Length; i++)
    //    {
    //        var field = postcodeFields[i].Field;
    //        field.Value = chars[i].ToString();

    //        field.SetDefaultFont("Arial", fontSize, Color.Black);
    //    }
    //}

    private static void FillPostcode(PdfDocument pdf, string postcode, int fontSize = 16)
    {
        var chars = postcode.Trim().ToUpper().ToCharArray();

        var postcodeFields = pdf.Form
            .Where(f => Regex.IsMatch(f.Name, @"^PostCode\[\d+\]$"))
            .Select(f => new
            {
                Field = f,
                Index = int.Parse(Regex.Match(f.Name, @"\d+").Value)
            })
            .OrderBy(x => x.Index)
            .ToList();

        for (int i = 0; i < postcodeFields.Count && i < chars.Length; i++)
        {
            var field = postcodeFields[i].Field;
            field.Value = chars[i].ToString();

            field.SetDefaultFont("Arial", fontSize, Color.Black);
        }
    }
}
