using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;

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

        public User DeleteUser(Guid user)
        {
            var existingUser = _eCommerceContext.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == user);


            if (existingUser != null)
            {
                var rolId = existingUser.Roles;
                while (rolId.Count > 0)
                {
                    var rol = _eCommerceContext.StringListWrappers.FirstOrDefault(r => r.Id == rolId[0].Id);
                    _eCommerceContext.StringListWrappers.Remove(rol);
                    _eCommerceContext.SaveChanges();
                }
                _eCommerceContext.Users.Remove(existingUser);
                _eCommerceContext.SaveChanges();
                return existingUser;
            }
            throw new DataAccessException("No users found");

        }

        public IEnumerable<User> GetAllUsers(Func<User, bool> predicate)
        {
            return _eCommerceContext.Users.Include(u => u.Roles).Where(predicate).ToList();
        }

        public User UpdateUser(User updatedUser)
        {
            var user = _eCommerceContext.Users.
                Include(u => u.Roles).
                Where(u => u.Id == updatedUser.Id).
                FirstOrDefault();

            if (user is null)
            {
                throw new DataAccessException($"User does not exist.");
            }
            else
            {
                if (updatedUser.Name is not null) user.Name = updatedUser.Name;
                if (updatedUser.Address is not null) user.Address = updatedUser.Address;
                if (updatedUser.Email is not null) user.Email = updatedUser.Email;
                if (updatedUser.Password is not null) user.Password = updatedUser.Password;
                if (updatedUser.Roles is not null) user.Roles = updatedUser.Roles;

                _eCommerceContext.SaveChanges();
                return user;
            }
        }
    }
}
