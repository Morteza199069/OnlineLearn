using OnlineLearn.Core.Services.Interfaces;
using OnlineLearn.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.Services
{
    public class UserService : IUserService
    {
        private readonly OnlineLearnContext _context;

        public UserService(OnlineLearnContext context)
        {
            _context = context;
        }
        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
    }
}
