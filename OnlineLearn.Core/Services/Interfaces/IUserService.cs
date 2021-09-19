using OnlineLearn.Core.DTOs;
using OnlineLearn.Core.DTOs.User;
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
        #region UserAccount 
        bool IsExistUserName(string username);
        bool IsExistEmail(string email);
        int AddUser(User user);
        User LoginUser(LoginVM login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string username);
        void UpdateUser(User user);
        bool ActiveAccount(string activeCode);
        #endregion

        #region UserPanel
        UserInformationVM GetUserInformation(string username);
        UserPanelSideBarDataVM GetUserPanelSideBarData(string username);
        #endregion
    }
}
