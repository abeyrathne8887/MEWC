using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEWC.API.Doamin.Module.Interface
{
    public interface ILoginTokenInfo
    {
        string AccessToken { get; set; }
        string Token { get; set; }
        string LoginId { get; set; }
        DateTime LastActivityTime { get; set; }
        string LastActivity { get; set; }
        UserInfo UserProfile { get; set; }
        Role[] Roles { get; set; }
        Permission[] Permissions { get; set; }
        bool Valid { get; set; }
    }
}
