using System.Text.Json.Serialization;

namespace IronPdfTest.Models;
public class PdfFieldMapping
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("x")]
    public float X { get; init; }

    [JsonPropertyName("y")]
    public float Y { get; init; }

    [JsonPropertyName("spacing")]
    public float? Spacing { get; init; }

    public float FontSize { get; init; } = 10;

    [JsonPropertyName("isIncludedExcludedOrNone")]
    public string? IsIncludedExcludedOrNone { get; init; }
}
