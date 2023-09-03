using System;

namespace BackEnd
{
    public class HelperValidator
    {
        public static bool IsAlphanumerical(object nonAlphanumericalName)
        {
            return false;
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