namespace Application.Common
{
    public static class ContentHelper
    {
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.ToLowerInvariant();

            text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-z0-9\s-]", "");

            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "-").Trim('-');

            return text.Length > 100 ? text.Substring(0, 100) : text;
        }

        public static string GenerateExcerpt(string content, int maxLength = 300)
        {
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;

            return content.Length > maxLength ? content.Substring(0, maxLength) : content;
        }
    }
}
