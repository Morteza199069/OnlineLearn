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
    }
}
