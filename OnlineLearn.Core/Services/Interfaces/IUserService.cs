using OnlineLearn.Core.DTOs;
using OnlineLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string username);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginVM login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        void UpdateUser(User user);
        bool ActiveAccount(string activeCode);
    }
}
