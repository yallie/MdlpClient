namespace MdlpApiClient.DataContracts
{
    using System;

    /// <summary>
    /// Custom <see cref="System.DateTime"/> wrapper class to work around serialization issues.
    /// Some API methods assume that date+time fields look like this: "2018-12-12T00:00:00Z".
    /// While some others assume that date+time fields look like this: "2018-12-12 00:00:00"
    /// </summary>
    public class CustomDateTime
    {
        /// <summary>
        /// Real DateTime value.
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            // 2020-04-24T21:01:40Z
            return DateTime.HasValue ? DateTime.Value.ToString("s") + "Z" : "null";
        }

        /// <summary>
        /// Parses the given string.
        /// </summary>
        /// <param name="s">String to parse.</param>
        /// <returns><see cref="CustomDateTime"/> instance.</returns>
        public static CustomDateTime Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || "null".Equals(s, StringComparison.OrdinalIgnoreCase))
            {
                return new CustomDateTime();
            }

            return new CustomDateTime
            {
                DateTime = System.DateTime.ParseExact(s.TrimEnd('z', 'Z'), "s", null)
            };
        }

        /// <summary>
        /// Implicit conversion to the <see cref="DateTime"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDateTime"/> instance.</param>
        public static implicit operator DateTime(CustomDateTime cd)
        {
            return cd.DateTime ?? default(DateTime);
        }

        /// <summary>
        /// Implicit conversion to the <see cref="Nullable{DateTime}"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDateTime"/> instance.</param>
        public static implicit operator DateTime?(CustomDateTime cd)
        {
            return cd.DateTime;
        }

        /// <summary>
        /// Implicit conversion to the <see cref="string"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDateTime"/> instance.</param>
        public static implicit operator string(CustomDateTime cd)
        {
            return cd == null ? "null" : cd.ToString();
        }

        /// <summary>
        /// Implicit conversion from the <see cref="Nullable{DateTime}"/> type.
        /// </summary>
        /// <param name="dt"><see cref="CustomDateTime"/> instance.</param>
        public static implicit operator CustomDateTime(DateTime? dt)
        {
            return new CustomDateTime { DateTime = dt };
        }
    }
}
