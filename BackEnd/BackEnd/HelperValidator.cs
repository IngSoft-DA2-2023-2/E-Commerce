using System;

namespace BackEnd
{
    public class HelperValidator
    {
        public static bool IsLengthBetween(string name, int minLength, int maxLength)
        {
            return name.Length >= minLength && name.Length <= maxLength;
        }
    }
}