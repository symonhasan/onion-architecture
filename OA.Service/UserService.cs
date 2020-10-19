using OA.Data;
using OA.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OA.Service
{
    public class UserService : IUserService
    {
        private IRepository<User> userRepository;

        public UserService(IRepository<User> _userRepository)
        {
            userRepository = _userRepository;
        }

        public User GetUser(string email)
        {
            IEnumerable<User> users = userRepository.GetAll();
            return users.FirstOrDefault(user => user.Email == email);
        }
    }
}
