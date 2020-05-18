namespace MdlpApiClient.DataContracts
{
    using System;

    /// <summary>
    /// Custom <see cref="System.DateTime"/> wrapper class to work around serialization issues.
    /// Some API methods assume that date+time fields look like this: "2018-12-12T00:00:00Z".
    /// While some others assume that date+time fields look like this: "2018-12-12 00:00:00"
    /// </summary>
    public class CustomDateTimeSpace
    {
        /// <summary>
        /// Real DateTime value.
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            // 2020-04-24 21:01:40
            return DateTime.HasValue ? DateTime.Value.ToString("s").Replace("T", " ") : "null";
        }

        /// <summary>
        /// Parses the given string.
        /// </summary>
        /// <param name="s">String to parse.</param>
        /// <returns><see cref="CustomDateTimeSpace"/> instance.</returns>
        public static CustomDateTimeSpace Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || "null".Equals(s, StringComparison.OrdinalIgnoreCase))
            {
                return new CustomDateTimeSpace();
            }

            return new CustomDateTimeSpace
            {
                DateTime = System.DateTime.ParseExact(s.Replace(" ", "T").TrimEnd('z', 'Z'), "s", null)
            };
        }

        /// <summary>
        /// Implicit conversion to the <see cref="DateTime"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDateTimeSpace"/> instance.</param>
        public static implicit operator DateTime(CustomDateTimeSpace cd)
        {
            return cd.DateTime ?? default(DateTime);
        }

        /// <summary>
        /// Implicit conversion to the <see cref="Nullable{DateTime}"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDateTimeSpace"/> instance.</param>
        public static implicit operator DateTime?(CustomDateTimeSpace cd)
        {
            return cd.DateTime;
        }

        /// <summary>
        /// Implicit conversion to the <see cref="string"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDateTimeSpace"/> instance.</param>
        public static implicit operator string(CustomDateTimeSpace cd)
        {
            return cd == null ? "null" : cd.ToString();
        }

        /// <summary>
        /// Implicit conversion from the <see cref="Nullable{DateTime}"/> type.
        /// </summary>
        /// <param name="dt"><see cref="CustomDateTimeSpace"/> instance.</param>
        public static implicit operator CustomDateTimeSpace(DateTime? dt)
        {
            return new CustomDateTimeSpace { DateTime = dt };
        }
    }
}
