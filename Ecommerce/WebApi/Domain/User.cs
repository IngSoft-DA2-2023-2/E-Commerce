using WebApi.Utilities;
using WebApi.ExceptionBackEnd;

namespace WebApi.Domain
{
    public class User
    {
        private const int _nameMinimumLength = 3;
        private const int _nameMaximumLength = 20;
        private const int _passwordMinimumLength = 5;
        private const int _passwordMaximumLength = 20;
        protected string _name;
        protected string _password;
        protected string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                ValidateEmail(value);
                _email = value;
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                ValidateName(value);
                _name = value;
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                ValidatePassword(value);
                _password = value;
            }
        }
        private static void ValidateEmail(string value)
        {
            if (!HelperValidator.IsValidEmail(value))
            {
                throw new BackEndException("email format is not valid");
            }
        }

        private static void ValidatePassword(string value)
        {
            if (!HelperValidator.IsLengthBetween(value, _passwordMinimumLength, _passwordMaximumLength))
            {
                throw new BackEndException($"Password length must be between {_passwordMinimumLength} and {_passwordMaximumLength}");
            }
        }

        private static void ValidateName(string value)
        {
            if (!HelperValidator.IsLengthBetween(value, _nameMinimumLength, _nameMaximumLength))
            {
                throw new BackEndException($"Name length must be between {_nameMinimumLength} and {_nameMaximumLength}");
            }
            if (!HelperValidator.IsAlphanumerical(value))
            {
                throw new BackEndException("Name must be alphanumerical");
            }
        }


    }
}