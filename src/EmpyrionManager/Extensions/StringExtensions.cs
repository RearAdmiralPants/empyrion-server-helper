namespace EmpyrionManager.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public static class StringExtensions
    {
        public static string TrailingString(this string orig, string trail)
        {
            if (!orig.ToLowerInvariant().EndsWith(trail.ToLowerInvariant()))
            {
                return orig + trail;
            }
            return orig;
        }

        public static string TrailingBackslash(this string orig)
        {
            return TrailingString(orig, "\\");
        }

        public static bool ContainsInvalidPathCharacter(this string orig)
        {
            var invalid = Path.GetInvalidPathChars();
            var fileInvalid = Path.GetInvalidFileNameChars();

            var both = new List<char>();
            both.AddRange(invalid);
            both.AddRange(fileInvalid);

            foreach (var badChar in both)
            {
                if (orig.Contains(badChar.ToString()))
                {
                    return true;
                }
                else
                {
                    //System.Windows.Forms.MessageBox.Show(orig + " doesn't contain '" + badChar.ToString() + "'!");
                }
            }

            return false;
        }

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
