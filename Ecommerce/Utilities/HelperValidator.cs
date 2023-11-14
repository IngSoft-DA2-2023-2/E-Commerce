using System.Text.RegularExpressions;

namespace Utilities
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

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}