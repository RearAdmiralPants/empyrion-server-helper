﻿namespace EmpyrionManager.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class StringExtensions
    {
        /// <summary>
        /// Adds a trailing string to a given string if it doesn't already end with that string.
        /// </summary>
        /// <param name="orig">The string to modify if it doesn't end with <paramref name="trail"/></param>
        /// <param name="trail">The string to append to <paramref name="orig"/> if it isn't already present at the end</param>
        /// <returns>The original string if it was not modified, or the modified string</returns>
        public static string TrailingString(this string orig, string trail)
        {
            if (!orig.ToLowerInvariant().EndsWith(trail.ToLowerInvariant()))
            {
                return orig + trail;
            }
            return orig;
        }

        /// <summary>
        /// Adds a trailing backslash to a given string if it doesn't already end with one.
        /// </summary>
        /// <param name="orig">The string to modify if needed.</param>
        /// <returns>The string with a trailing backslash.</returns>
        public static string TrailingBackslash(this string orig)
        {
            return TrailingString(orig, "\\");
        }

        /// <summary>
        /// Returns a value indicating whether a string contains an invalid path character.
        /// </summary>
        /// <param name="orig">The string to search for invalid characters.</param>
        /// <returns>A value indicating whether <paramref name="orig"/> contains an invalid character.</returns>
        public static bool ContainsInvalidPathCharacter(this string orig)
        {
            var invalid = Path.GetInvalidPathChars();

            foreach (var badChar in invalid)
            {
                if (orig.Contains(badChar.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a string contains an invalid file character.
        /// </summary>
        /// <param name="orig">The string to search for invalid characters.</param>
        /// <returns>A value indicating whether <paramref name="orig"/> contains an invalid character.</returns>
        public static bool ContainsInvalidFileCharacter(this string orig)
        {
            var invalid = Path.GetInvalidFileNameChars();
            foreach (var badChar in invalid)
            {
                if (orig.Contains(badChar.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
