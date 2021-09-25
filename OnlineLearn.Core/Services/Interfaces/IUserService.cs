using OnlineLearn.Core.DTOs;
using OnlineLearn.DataLayer.Entities.User;
using OnlineLearn.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;

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
        int GetUserIdByUserName(string username);
        void UpdateUser(User user);
        bool ActiveAccount(string activeCode);
        #endregion

        #region UserPanel
        UserInformationVM GetUserInformation(string username);
        UserPanelSideBarDataVM GetUserPanelSideBarData(string username);
        EditProfileVM GetUserDataToEditProfile(string username);
        void EditProfile(string username, EditProfileVM profile);
        bool CompareOldPassword(string username, string oldPassword);
        void ChangeUserPassword(string username, string newPassword);
        #endregion

        #region Wallet
        int UserWalletBalance(string username);
        List<WalletVM> GetUserWallet(string username);
        int ChargeWallet(string username, int amount, string description, bool isPay = false);
        int AddWallet(Wallet wallet);
        Wallet GetWalletByWalletId(int walletId);
        void UpdateWallet(Wallet wallet);
        #endregion

        #region Admin Panel
        UsersInAdminVM GetUsers(int pageId = 1, string filterEmail = "", string filterUsername = "");
        int AddUserFromAdmin(CreateUserVM user);
        #endregion
    }
}
