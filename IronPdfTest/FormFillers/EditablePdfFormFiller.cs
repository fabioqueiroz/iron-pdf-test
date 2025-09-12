namespace IronPdfTest.FormFillers;

internal class EditablePdfFormFiller
{
    internal static byte[] FillAndLockForm(PdfDocument pdf)
    {
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
}
