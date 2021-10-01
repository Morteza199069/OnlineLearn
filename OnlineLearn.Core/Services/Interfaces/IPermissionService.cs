using OnlineLearn.DataLayer.Entities.Permissions;
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
        #region Roles
        List<Role> GetRoles();
        int AddRole(Role role);
        Role GetRoleById(int roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void AddRolesToUser(List<int> roleIds, int userId);
        void EditUserRoles(int userId, List<int> roleIds);
        #endregion

        #region Permissions
        List<Permission> GetAllPermissions();
        void AddPermissionsToRole(int roleId, List<int> permission);
        List<int> PermissionRoles(int roleId);
        void UpdatePermissionRoles(int roleId, List<int> permissions);
        bool CheckPermission(int permissionId, string username);
        #endregion
    }
}
