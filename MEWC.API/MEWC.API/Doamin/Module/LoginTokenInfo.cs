using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEWC.API.Doamin.Module
{
    public class LoginTokenInfo //: ILoginTokenInfo
    {
        public string AccessToken { get; set; }
        public string Token { get; set; }
        public string LoginId { get; set; }
        public DateTime LastActivityTime { get; set; }
        public string LastActivity { get; set; }
        public UserInfo UserProfile { get; set; }
        public Role[] Roles { get; set; }
        public Permission[] Permissions { get; set; }
        public bool Valid { get; set; }

    }

    public class UserInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string ContactNo { get; set; }
        public string UserType { get; set; }
    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class Permission
    {
        public int PermissionId { get; set; }
        public string ModuleName { get; set; }
        public string PermissionName { get; set; }
    }
}
