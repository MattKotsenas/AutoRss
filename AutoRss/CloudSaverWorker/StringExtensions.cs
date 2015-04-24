using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoRss.CloudSaverWorker
{
    internal static class StringExtensions
    {
        public static string ToShaHash(this string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            using (var sha = new SHA256Managed())
            {
                var data = Encoding.UTF8.GetBytes(text);
                var hash = sha.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant();
            }
        }
    }
}
