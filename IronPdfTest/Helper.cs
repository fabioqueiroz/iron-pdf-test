using System.Text.Json.Nodes;

namespace IronPdfTest;

internal static class Helper
{
    internal static string? GetValueFromJson(JsonObject root, string path)
    {
        var parts = path.Split('.');
        JsonNode? current = root;

        foreach (var part in parts)
        {
            if (current == null) return null;
            current = current[part];
        }

        return current?.ToString();
    }

    internal static string? GetCheckboxValueFromJson(JsonObject root, string path, string nodeProperty)
    {
        var pathValue = GetValueFromJson(root, path);
        var choice = string.Empty;

        if (!string.IsNullOrWhiteSpace(pathValue) && pathValue.StartsWith("{"))
        {
            try
            {
                var node = JsonNode.Parse(pathValue)!.AsObject();
                choice = node[nodeProperty]?.ToString() ?? string.Empty;
            }
            catch
            {
                choice = string.Empty;
            }
        }

        return choice;
    }

    internal static string? PostCodeFormatter(string postcode)
    {
        return string.Join("", postcode.Where(c => !char.IsWhiteSpace(c)));
    }
}
