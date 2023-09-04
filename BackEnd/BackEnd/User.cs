using System;
using System.Runtime.InteropServices;

namespace BackEnd
{
    public class User
    {
        private const int _nameMinimumLength = 3;
        private const int _nameMaximumLength = 20;
        protected string _name;
        protected string _password;
        public string Email { get; set; }
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
                if (value.Length < 5)
                {
                    throw new BackEndException("password is too short");
                }
                if (value.Length > 20)
                {
                    throw new BackEndException("password is too long");
                }
                _password = value;
            }
        }

        private void ValidateName(string value)
        {
            if (!HelperValidator.IsLengthBetween(value, _nameMinimumLength, _nameMaximumLength))
            {
                throw new BackEndException($"Name length must be between {_nameMinimumLength} and {_nameMaximumLength}");
            }
            if(!HelperValidator.IsAlphanumerical(value))
            {
                throw new BackEndException("Name must be alphanumerical");
            }
        }


    }
}