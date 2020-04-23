namespace MdlpApiClient.Serialization
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using MdlpApiClient.Xsd;

    /// <summary>
    /// XML serialization helper.
    /// </summary>
    public static class XmlSerializationHelper
    {
        /// <summary>
        /// Deserializes the given XML document.
        /// </summary>
        /// <param name="docXml">XML document to deserialize.</param>
        /// <returns>The <see cref="Documents"/> instance.</returns>
        public static Documents Deserialize(string docXml)
        {
            var serializer = new XmlSerializer(typeof(Documents));
            using (var reader = new StringReader(docXml))
            {
                return serializer.Deserialize(reader) as Documents;
            }
        }

        /// <summary>
        /// Serializes the given document to XML string.
        /// </summary>
        /// <param name="doc">Document to serialize.</param>
        /// <param name="comments">Optional comments such as application name and version.</param>
        /// <returns>Serialized XML document.</returns>
        public static string Serialize(Documents doc, string comments = null)
        {
            var serializer = new XmlSerializer(typeof(Documents));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, doc);

                // add optional comments, etc
                var xml = writer.GetStringBuilder().ToString();
                var xdoc = XDocument.Parse(xml);

                // add namespace prefix
                var by = new XAttribute(XNamespace.Xmlns + "by", PackageUrl);
                xdoc.Root.ReplaceAttributes(new object[] { by, xdoc.Root.Attributes() });

                // add optional comments
                if (!string.IsNullOrWhiteSpace(comments))
                {
                    var element = xdoc.FirstNode as XElement;
                    if (element != null && element.FirstNode != null)
                    {
                        element.FirstNode.AddBeforeSelf(new XComment(comments));
                    }
                }

                return xdoc.ToXmlString();
            }
        }

        /// <summary>
        /// Saves <see cref="XDocument"/> as string preserving the declaration node.
        /// </summary>
        /// <param name="xdoc"><see cref="XDocument"/> to save.</param>
        /// <param name="options"><see cref="SaveOptions"/>, optional.</param>
        /// <returns>String representation of the given <see cref="XDocument"/>.</returns>
        public static string ToXmlString(this XDocument xdoc, SaveOptions options = SaveOptions.None)
        {
            var newLine = (options & SaveOptions.DisableFormatting) == SaveOptions.DisableFormatting ? "" : Environment.NewLine;
            return xdoc.Declaration == null ? xdoc.ToString(options) : xdoc.Declaration + newLine + xdoc.ToString(options);
        }

        internal static string PackageUrl = GetPackageUrl();

        private static string GetPackageUrl()
        {
            // get assembly file version, such as 1.0.2.4
            var version = typeof(MdlpClient).Assembly
                .GetCustomAttributes(typeof(AssemblyFileVersionAttribute))
                .OfType<AssemblyFileVersionAttribute>()
                .FirstOrDefault()
                .Version;

            // skip the last part: 1.2.3.x => 1.2.3
            var index = version.LastIndexOf(".");
            return "https://www.nuget.org/packages/MdlpApiClient/" + version.Substring(0, index);
        }
    }
}
