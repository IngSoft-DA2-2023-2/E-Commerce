using DataAccess.Context;
using DataAccessInterface.Exceptions;
using DataAccessInterface;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceContext _eCommerceContext;
        public UserRepository(ECommerceContext context)
        {
            _eCommerceContext = context;
        }
        public User CreateUser(User user)
        {
            if (_eCommerceContext.Users.FirstOrDefault(p => p.Email.Equals(user.Email)) is null)
            {
                _eCommerceContext.Users.Add(user);
                _eCommerceContext.SaveChanges();
                return user;
            }
            throw new DataAccessException($"User with email {user.Email} already exists.");
        }

        public User DeleteUser(User user)
        {
            var existingUser = _eCommerceContext.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                _eCommerceContext.Users.Remove(existingUser);
                _eCommerceContext.SaveChanges();
                return existingUser;
            }
            throw new DataAccessException($"No users found");

        }

        public IEnumerable<User> GetAllUsers(Func<User, bool> predicate)
        {
            return _eCommerceContext.Users.Where(predicate).ToList();
        }

        public User UpdateUser(User updatedUser)
        {
            var existingUser = _eCommerceContext.Users.FirstOrDefault(u => u.Email == updatedUser.Email);
            if (existingUser != null)
            {
              if(!updatedUser.Address.IsNullOrEmpty()) existingUser.Address = updatedUser.Address;
              if(!updatedUser.Password.IsNullOrEmpty()) existingUser.Password = updatedUser.Password;
              if(!updatedUser.Roles.Any()) existingUser.Roles = updatedUser.Roles;
              if(!updatedUser.Name.IsNullOrEmpty()) existingUser.Name = updatedUser.Name;

                _eCommerceContext.Update(existingUser);

                return _eCommerceContext.Users.FirstOrDefault(u => u.Email == updatedUser.Email);
            }
            throw new DataAccessException("No user found");
        }
    }
}
