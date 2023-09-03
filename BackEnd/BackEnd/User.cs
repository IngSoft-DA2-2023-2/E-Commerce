using System;
using System.Runtime.InteropServices;

namespace BackEnd
{
    public class User
    {
        private const int _nameMinimumLength = 3;
        private const int _nameMaximumLength = 20;
        protected string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                validateName(value);
                _name = value;
            }
        }

        private void validateName(string value)
        {
            if (!HelperValidator.IsLengthBetween(value, _nameMinimumLength, _nameMaximumLength))
            {
                throw new BackEndException($"Name length must be between {_nameMinimumLength} and {_nameMaximumLength}");
            }
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}