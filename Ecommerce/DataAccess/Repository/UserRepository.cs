using DataAccess.Context;
using DataAccess.Exceptions;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public bool Exist(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
