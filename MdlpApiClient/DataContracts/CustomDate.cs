namespace MdlpApiClient.DataContracts
{
    using System;

    /// <summary>
    /// Custom <see cref="System.DateTime"/> wrapper class to work around serialization issues.
    /// Some API methods assume that date+time fields look like this: "2018-12-12T00:00:00Z".
    /// While some others assume that date+time fields look like this: "2018-12-12 00:00:00"
    /// </summary>
    public class CustomDate
    {
        /// <summary>
        /// Real Date value.
        /// </summary>
        public DateTime? DateTime { get; set; }

        private const string Format = "yyyy\\-MM\\-dd";

        /// <inheritdoc/>
        public override string ToString() => // 2020-04-24
            DateTime.HasValue ? DateTime.Value.ToString(Format) : null;

        /// <summary>
        /// Parses the given string.
        /// </summary>
        /// <param name="s">String to parse.</param>
        /// <returns><see cref="CustomDate"/> instance.</returns>
        public static CustomDate Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || "null".Equals(s, StringComparison.OrdinalIgnoreCase))
            {
                return new CustomDate();
            }

            return new CustomDate
            {
                DateTime = System.DateTime.ParseExact(s.TrimEnd('z', 'Z'), Format, null).Date
            };
        }

        /// <summary>
        /// Implicit conversion to the <see cref="DateTime"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDate"/> instance.</param>
        public static implicit operator DateTime(CustomDate cd) =>
            cd.DateTime ?? default(DateTime);

        /// <summary>
        /// Implicit conversion to the <see cref="Nullable{DateTime}"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDate"/> instance.</param>
        public static implicit operator DateTime?(CustomDate cd) => cd.DateTime;

        /// <summary>
        /// Implicit conversion to the <see cref="string"/> type.
        /// </summary>
        /// <param name="cd"><see cref="CustomDate"/> instance.</param>
        public static implicit operator string(CustomDate cd) =>
            cd == null ? null : cd.ToString();

        /// <summary>
        /// Implicit conversion from the <see cref="Nullable{DateTime}"/> type.
        /// </summary>
        /// <param name="dt"><see cref="CustomDate"/> instance.</param>
        public static implicit operator CustomDate(DateTime? dt) =>
            new CustomDate { DateTime = dt };

        /// <summary>
        /// Implicit conversion from the <see cref="DateTime"/> type.
        /// </summary>
        /// <param name="dt"><see cref="CustomDate"/> instance.</param>
        public static implicit operator CustomDate(DateTime dt) =>
            new CustomDate { DateTime = dt };
    }
}
