using System.Runtime.InteropServices;

namespace BackEnd
{
    public class User
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(!HelperValidator.IsLengthBetween(value,2,10)) {
                    throw new BackEndException("name is too long");
                }
                else
                {
                      _name = value;
                }
              
            }
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}