using System.Text;

namespace ACC.WebApp.Client.Components.Pages.Guia.Components.Examenes;

internal static class ExamTextNormalizer
{
    public static string Normalize(string? value, string fallback = "")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return fallback;
        }

        if (!LooksLikeMojibake(value))
        {
            return value;
        }

        try
        {
            var decoded = Encoding.UTF8.GetString(Encoding.Latin1.GetBytes(value));
            return GetMojibakeScore(decoded) < GetMojibakeScore(value) ? decoded : value;
        }
        catch
        {
            return value;
        }
    }

    public static List<string> NormalizeMany(IEnumerable<string?> values)
        => values
            .Select(value => Normalize(value))
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .ToList();

    private static bool LooksLikeMojibake(string value)
        => value.Contains('Ã') || value.Contains('Â') || value.Contains('Ð') || value.Contains('�');

    private static int GetMojibakeScore(string value)
        => value.Count(ch => ch is 'Ã' or 'Â' or 'Ð' or '�');
}
