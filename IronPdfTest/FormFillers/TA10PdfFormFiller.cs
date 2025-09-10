using IronPdfTest.Models;
using System.Drawing;
using System.Text.Json.Nodes;

namespace IronPdfTest.FormFillers;

internal class TA10PdfFormFiller
{
    private const string PdfOutputFileName = "TA10_Filled_Sample.pdf";

    internal static void FillForm(PdfDocument pdf, JsonObject transactionFormData, Dictionary<string, PdfFieldMapping> fieldMappings)
    {
        foreach (var mapping in fieldMappings!)
        {
            var jsonPath = mapping.Key;
            var fieldMapping = mapping.Value;

            MapFillerRules(pdf, transactionFormData, fieldMapping, jsonPath);
        }

        pdf.SaveAs(PdfOutputFileName);
    }

    private static void MapFillerRules(PdfDocument pdf, JsonObject transactionFormData, PdfFieldMapping fieldMapping, string jsonPath)
        => new List<(Func<string, bool> Predicate, Action<PdfDocument, JsonObject, PdfFieldMapping, string> Output)>
        {
           new (jsonPath => jsonPath.Contains("postcode"), 
               (pdf, transactionFormData, fieldMapping, jsonPath) 
               => FillPostCode(pdf, transactionFormData, fieldMapping, jsonPath)),

           new (jsonPath => jsonPath.Contains("fixturesAndFittings"), 
               (pdf, transactionFormData, fieldMapping, jsonPath) 
               => FillCheckBox(pdf, transactionFormData, fieldMapping, jsonPath)),

           new (_ => true, 
               (pdf, transactionFormData, fieldMapping, jsonPath) 
               => FillTextBox(pdf, transactionFormData, fieldMapping, jsonPath))

        }.First(x => x.Predicate(jsonPath)).Output(pdf, transactionFormData, fieldMapping, jsonPath);

    private static void FillTextBox(PdfDocument pdf, JsonObject transactionFormData, PdfFieldMapping fieldMapping, string jsonPath)
    {
        var text = Helper.GetValueFromJson(transactionFormData!, jsonPath) ?? string.Empty;
        TextBoxFiller(pdf, text, fieldMapping);
    }

    private static void FillCheckBox(PdfDocument pdf, JsonObject transactionFormData, PdfFieldMapping fieldMapping, string jsonPath)
    {
        var choice = Helper.GetCheckboxValueFromJson(transactionFormData, jsonPath, "isIncludedExcludedOrNone") ?? string.Empty;
        CheckBoxFiller(pdf, jsonPath, choice, fieldMapping);
    }

    private static void FillPostCode(PdfDocument pdf, JsonObject transactionFormData, PdfFieldMapping fieldMapping, string jsonPath)
    {
        var postcode = Helper.GetValueFromJson(transactionFormData, jsonPath) ?? string.Empty;
        PostCodeFiller(pdf, jsonPath, postcode, fieldMapping);
    }

    private static void TextBoxFiller(PdfDocument pdf, string text, PdfFieldMapping fieldMapping)
    {
        DrawTextToPdfFile(pdf, text, fieldMapping.Page, fieldMapping.X, fieldMapping.Y);
    }

    private static void PostCodeFiller(PdfDocument pdf, string jsonPath, string postcode, PdfFieldMapping fieldMapping)
    {
        var formattedPostCode = Helper.PostCodeFormatter(postcode);
        float xStart = fieldMapping.X;
        float y = fieldMapping.Y;
        float step = (float)fieldMapping.Spacing!;

        for (int i = 0; i < formattedPostCode?.Length; i++)
        {
            DrawTextToPdfFile(pdf, text: formattedPostCode[i].ToString(), fieldMapping.Page, x: xStart + (i * step), y: y);
        }
    }

    private static void CheckBoxFiller(PdfDocument pdf, string jsonPath, string choice, PdfFieldMapping fieldMapping)
    {
        float offset = choice switch
        {
            "Included" => 0,
            "Excluded" => 47,
            "None" => 94,
            _ => 0
        };

        float xStart = fieldMapping.X;
        float y = fieldMapping.Y;
        float step = (float)fieldMapping.Spacing! + offset;

        DrawTextToPdfFile(pdf, text: "X", fieldMapping.Page, x:xStart + step, y: y);
    }

    private static void DrawTextToPdfFile(PdfDocument pdf, string text, int pageIndex, float x, float y)
    {
        pdf.DrawText(
            Text: text,
            FontName: "Arial",
            FontSize: 10,
            PageIndex: pageIndex,
            X: x,
            Y: y,
            Color: Color.Black,
            Rotation: 0.0
        );
    }
}
