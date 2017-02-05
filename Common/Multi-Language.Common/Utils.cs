using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace Multi_language.Common
{
    /// <summary>
    /// Helper methods and other junk
    /// </summary>
    public static class Utils
    {
        #region JSON routines


        #endregion

        /// <summary>
        /// Checks if caller application launched with elevated Administrative privileges
        /// </summary>
        /// <returns>True if caller launched with privileges elevated to Administrator, otherwise false</returns>
        public static bool IsElevatedToAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Helper method to get an local IPv4 address
        /// </summary>
        /// <returns>An IPAddress instance from the IPv4 address family</returns>
        public static IPAddress GetLocalIpV4()
        {
            return GetIpAddressIpV4(Dns.GetHostName()).FirstOrDefault();
        }

        /// <summary>
        /// Helper method to get an IPv4 Address
        /// </summary>
        /// <param name="hostname">The ip address string or hostname</param>
        /// <returns>An IPAddress instance from the IPv4 address family</returns>
        public static IEnumerable<IPAddress> GetIpAddressIpV4(string hostname)
        {
            return
                Dns.GetHostEntry(hostname)
                    .AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        public static Uri CombineUrl(string baseUrl, string relativeUrl)
        {
            Uri baseUri = !baseUrl.EndsWith("/") ? new Uri(baseUrl + "/") : new Uri(baseUrl);
            return new Uri(baseUri, relativeUrl.TrimStart('/'));
        }

        public static string GetFileSizeString(string filename)
        {
            long len = new FileInfo(filename).Length;
            return GetSizeString(len);
        }

        public static string GetSizeString(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };

            int order = 0;
            while (size >= 1024 && ++order < sizes.Length)
            {
                size = size / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return $"{size:0.#} {sizes[order]}";
        }

        public static double GetDiskSpaceUsedPercentage(string driveLetter = "c")
        {
            var allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && d.Name.Equals($"{driveLetter}:\\", StringComparison.OrdinalIgnoreCase))
                {
                    return (d.TotalSize - d.TotalFreeSpace) * 100.0 / d.TotalSize;
                }
            }
            return 0;
        }

        public static string GetDiskSpaceUsedString(string driveLetter = "c")
        {
            var allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && d.Name.Equals($"{driveLetter}:\\", StringComparison.OrdinalIgnoreCase))
                {
                    return $"{GetSizeString(d.TotalSize - d.TotalFreeSpace)} used of {GetSizeString(d.TotalSize)}";
                }
            }
            return string.Empty;
        }

        public static string GetSystemUpTime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                uptime.NextValue();       // Call this an extra time before reading its value
                return TimeSpan.FromSeconds(uptime.NextValue()).ToPrettyFormat();
            }
        }

        /// <summary>
        /// Gets a string and add  + " ('next counter index')"
        /// </summary>
        /// <param name="name">Name for changing</param>
        /// <param name="existingNames">List with already defined names</param>
        /// <returns>name (1)</returns>
        public static string CreateNameCopy(string name, List<string> existingNames)
        {
            var trimmedName = name.Trim();
            const string regexString = @"^\(\d+\)$";
            var regex = new Regex(regexString);

            var startingWithExludingName = existingNames.
                Where(_ => _.StartsWith(trimmedName, StringComparison.OrdinalIgnoreCase)).
                Select(_ => _.Substring(trimmedName.Length).Trim()).
                Where(_ => !string.IsNullOrEmpty(_));

            var copies = startingWithExludingName.Where(_ => regex.IsMatch(_));

            // For each match, get the number between brackets, then get the first 'counter' value that is not in that list
            var existingIndexes = copies.Select(GetIndexFromName).ToList();
            var copyIndex = 1;
            while (existingIndexes.Any(_ => _ == copyIndex))
            {
                copyIndex++;
            }
            return $"{trimmedName} ({copyIndex})";
        }

        // Only expects correct input (so ending with "(\d+)")
        private static int GetIndexFromName(string name)
        {
            var start = name.LastIndexOf("(", StringComparison.Ordinal) + 1;
            var length = name.Length - start - 1;
            var numberString = name.Substring(start, length);

            return int.Parse(numberString);
        }

        /// <summary>
        /// Split a comma or semicolon separated list and trims each part. Empty entries are removed.
        /// </summary>
        /// <param name="valueString">string that should be split into a list (on each comma or semicolon)</param>
        /// <returns>The list of string (with spaces trimmed and empty items removed)</returns>
        public static IEnumerable<string> SplitCommaOrSemiColonSeperatedList(string valueString)
        {
            if (valueString == null)
            {
                return Enumerable.Empty<string>();
            }
            return valueString.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => _.Trim()).Where(_ => !string.IsNullOrEmpty(_));
        }


        /// <summary>
        /// Helper method to convert a DateTime to nice string representation that can be used in log statements.
        /// </summary>
        /// <param name="date">The DateTime we want to convert.</param>
        /// <returns>A short time indication that can be used in logmessages.</returns>
        public static string GetTimeString(DateTime date)
        {
            return date.ToString("HH:mm:ss,ffff");
        }

        public static IEnumerable<T1> GetKeysByValue<T1, T2>(this Dictionary<T1, T2> dico, T2 value)
        {
            return from kvp in dico where kvp.Value.Equals(value) select kvp.Key;
        }
    }

    public static class StringExtensions
    {
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }

    public static class TimeSpanExtensions
    {
        public static bool HasDays(this TimeSpan timeSpan)
        {
            return timeSpan.Days != 0;
        }

        public static bool HasMinutes(this TimeSpan timeSpan)
        {
            return timeSpan.Minutes != 0;
        }

        public static bool HasHours(this TimeSpan timeSpan)
        {
            return timeSpan.Hours != 0;
        }

        public static string ToPrettyFormat(this TimeSpan timeSpan)
        {
            var dayParts = new[] { GetDays(timeSpan), GetHours(timeSpan), GetMinutes(timeSpan) }
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            var numberOfParts = dayParts.Length;

            string result;
            if (numberOfParts < 2)
            {
                result = dayParts.FirstOrDefault() ?? string.Empty;
            }
            else
            {
                result = string.Join(", ", dayParts, 0, numberOfParts - 1) + " and " + dayParts[numberOfParts - 1];
            }

            return result.UppercaseFirst();
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private static string GetMinutes(TimeSpan timeSpan)
        {
            if (timeSpan.Minutes == 0) return string.Empty;
            if (timeSpan.Minutes == 1) return "a minute";
            return timeSpan.Minutes + " minutes";
        }

        private static string GetHours(TimeSpan timeSpan)
        {
            if (timeSpan.Hours == 0) return string.Empty;
            if (timeSpan.Hours == 1) return "an hour";
            return timeSpan.Hours + " hours";
        }

        private static string GetDays(TimeSpan timeSpan)
        {
            if (timeSpan.Days == 0) return string.Empty;
            if (timeSpan.Days == 1) return "a day";
            return timeSpan.Days + " days";
        }
    }
}
