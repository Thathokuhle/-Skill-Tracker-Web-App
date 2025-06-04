using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.URLEncryptionBusiness
{
    static class Base64Utils
    {
        public static bool IsBase64String(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return false;

            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
    }
}
