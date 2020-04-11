namespace MdlpApiClient.Toolbox
{
    using System;
    using System.Linq;

    /// <summary>
    /// Dependency-free JSON formatter.
    /// </summary>
    public static class JsonFormatter
	{
		/// <summary>
		/// Formats the given JSON code.
		/// </summary>
		/// <remarks>
		/// Based on:
		/// https://stackoverflow.com/a/57100143/544641
		/// https://stackoverflow.com/a/24782322/544641
		/// </remarks>
		/// <param name="json">JSON code to format.</param>
		/// <param name="indent">Optional indent.</param>
		public static string FormatJson(string json, string indent = "  ")
		{
			var indentation = 0;
			var quoteCount = 0;
			var escapeCount = 0;

			var result =
				from ch in json ?? string.Empty
				let escaped = (ch == '\\' ? escapeCount++ : escapeCount > 0 ? escapeCount-- : escapeCount) > 0
				let quotes = ch == '"' && !escaped ? quoteCount++ : quoteCount
				let unquoted = quotes % 2 == 0
				let colon = ch == ':' && unquoted ? ": " : null
				let nospace = char.IsWhiteSpace(ch) && unquoted ? string.Empty : null
				let lineBreak = ch == ',' && unquoted ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, indentation)) : null
				let openChar = (ch == '{' || ch == '[') && unquoted ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, ++indentation)) : ch.ToString()
				let closeChar = (ch == '}' || ch == ']') && unquoted ? Environment.NewLine + string.Concat(Enumerable.Repeat(indent, --indentation)) + ch : ch.ToString()
				select colon ?? nospace ?? lineBreak ?? (
					openChar.Length > 1 ? openChar : closeChar
				);

			return string.Concat(result);
		}
	}
}
