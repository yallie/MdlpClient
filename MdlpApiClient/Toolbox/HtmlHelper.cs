namespace MdlpApiClient.Toolbox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public static class HtmlHelper
    {
        /// <summary>
        /// Try to extract readable text from HTML.
        /// </summary>
        /// <param name="html">HTML to process.</param>
        /// <returns>Human-readable text.</returns>
        public static string ExtractText(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            var bodyStart = html.IndexOf("<body");
            if (bodyStart >= 0)
            {
                html = html.Substring(bodyStart);
            }

            // replace tags
            var text = new Regex("<(br/?)|(</h[1-6])>").Replace(html, Environment.NewLine);
            text = new Regex("<[^>]+>").Replace(text, " ");
            text = new Regex("[ \t]+").Replace(text, " ");

            // trim lines
            var lines = text.Split('\r').Select(l => l.Trim()).Where(l => !string.IsNullOrEmpty(l));
            return string.Join(Environment.NewLine, lines);
        }
    }
}
