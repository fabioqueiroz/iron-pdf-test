namespace IronPdfTest.FormFillers;

internal class EditablePdfFormFiller
{
    internal static void FillForm(PdfDocument pdf)
    {
        pdf.Form.SingleOrDefault(x => x.Name == "Name")!.Value = "TEST NAME";
        pdf.Form.SingleOrDefault(x => x.Name == "Name of Dependent")!.Value = "Full Test Name";
        pdf.Form.SingleOrDefault(x => x.Name == "Age\t of Dependent")!.Value = "66";
        pdf.Form.SingleOrDefault(x => x.Name == "Dropdown2")!.Value = "Choice 4";
        pdf.Form.SingleOrDefault(x => x.Name == "Option 1")!.Value = "On";
        pdf.Form.SingleOrDefault(x => x.Name == "Option 3")!.Value = "On";

        pdf.SaveAs("Sample_Filled.pdf");
    }
}
