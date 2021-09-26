using OnlineLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearn.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Permission
        List<Role> GetRoles();
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditUserRoles(int userId, List<int> roleIds);
        #endregion
    }
}
