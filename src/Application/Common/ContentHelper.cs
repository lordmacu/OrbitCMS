using Core.Entities;
using Core.Interfaces;

namespace Application.Common
{
    public static class ContentHelper
    {
        /// <summary>
        /// Generates a URL-friendly slug from the given text.
        /// </summary>
        /// <param name="text">The input text to convert into a slug.</param>
        /// <returns>
        /// A string representing the generated slug. If the input is null or whitespace,
        /// an empty string is returned. The slug is limited to a maximum of 100 characters.
        /// </returns>
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.ToLowerInvariant();

            text = System.Text.RegularExpressions.Regex.Replace(text, @"[^a-z0-9\s-]", "");

            text = System.Text.RegularExpressions.Regex.Replace(text, @"\s+", "-").Trim('-');

            return text.Length > 100 ? text.Substring(0, 100) : text;
        }


        /// <summary>
        /// Generates an excerpt from the given content by truncating it to a specified maximum length.
        /// </summary>
        /// <param name="content">The input content from which to generate the excerpt.</param>
        /// <param name="maxLength">The maximum length of the excerpt. Defaults to 300 characters.</param>
        /// <returns>
        /// A string representing the generated excerpt. If the input content is null, empty, or consists only of white-space characters,
        /// an empty string is returned. If the content length exceeds the specified maxLength, it is truncated to maxLength characters.
        /// Otherwise, the original content is returned unchanged.
        /// </returns>
        public static string GenerateExcerpt(string content, int maxLength = 300)
        {
            if (string.IsNullOrWhiteSpace(content))
                return string.Empty;

            return content.Length > maxLength ? content.Substring(0, maxLength) : content;
        }
    }
}
