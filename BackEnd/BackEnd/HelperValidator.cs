using System;
using System.Linq;
using System.Xml.Linq;

namespace BackEnd
{
    public class HelperValidator
    {
        public static bool IsAlphanumerical(string name)
        {
            return name.All(char.IsLetterOrDigit);
        }

        public static bool IsLengthBetween(string name, int minLength, int maxLength)
        {
            return name.Length >= minLength && name.Length <= maxLength;
        }

        public static bool IsTrimmable(string name)
        {
            return !name.Equals(name.Trim());
        }

    }
}