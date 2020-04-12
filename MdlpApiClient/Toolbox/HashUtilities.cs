using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MdlpApiClient.Toolbox
{
    /// <summary>
    /// Helper class for working with hashes.
    /// </summary>
    public static class HashUtilities
    {
        /// <summary>
        /// Returns the hex representation for an array of bytes (for example, md5-hash).
        /// </summary>
        /// <param name="hash">Byte array to convert.</param>
        public static string ToHexString(this byte[] hash)
        {
            return string.Join(string.Empty, (hash ?? EmptyBuffer).Select(b => string.Format("{0:x2}", b)));
        }

        /// <summary>
        /// Converts hexadecimal string such as md5 hash back to the array of bytes.
        /// </summary>
        /// <param name="hexString">Hexadecimal string, such as "cafebabe".</param>
        public static byte[] FromHexString(this string hexString)
        {
            if (string.IsNullOrWhiteSpace(hexString))
            {
                return EmptyBuffer;
            }

            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentOutOfRangeException("hexString", "Hexadecimal string length should be even.");
            }

            var result = new byte[hexString.Length / 2];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return result;
        }

        /// <summary>
        /// Computes the hash of an array of bytes.
        /// </summary>
        /// <typeparam name="T">Hash algorithm.</typeparam>
        /// <param name="data">The data to hash.</param>
        public static string ComputeHash<T>(this byte[] data)
            where T : HashAlgorithm
        {
            var algorithm = typeof(T).FullName;
            var hasher = (T)CryptoConfig.CreateFromName(algorithm);
            var hash = hasher.ComputeHash(data);
            return hash.ToHexString();
        }

        /// <summary>
        /// Empty byte array.
        /// </summary>
        public static readonly byte[] EmptyBuffer = new byte[0];

        /// <summary>
        /// Computes the hash of the specified string.
        /// </summary>
        /// <typeparam name="T">Hash algorithm.</typeparam>
        /// <param name="str">The string to hash.</param>
        public static string ComputeHash<T>(this string str)
            where T : HashAlgorithm
        {
            var bytes = EmptyBuffer;
            if (!string.IsNullOrEmpty(str))
            {
                bytes = Encoding.Default.GetBytes(str);
            }

            var algorithm = typeof(T).FullName;
            var hasher = (T)CryptoConfig.CreateFromName(algorithm);
            bytes = hasher.ComputeHash(bytes);
            return bytes.ToHexString();
        }

        /// <summary>
        /// Computes the hash for the stream.
        /// </summary>
        /// <typeparam name="T">Hash algorithm.</typeparam>
        /// <param name="fs"><see cref="Stream"/> of bytes to hash.</param>
        public static string ComputeHash<T>(this Stream fs)
            where T : HashAlgorithm
        {
            var algorithm = typeof(T).FullName;
            var hasher = (T)CryptoConfig.CreateFromName(algorithm);
            var hash = hasher.ComputeHash(fs);
            return hash.ToHexString();
        }
    }
}
