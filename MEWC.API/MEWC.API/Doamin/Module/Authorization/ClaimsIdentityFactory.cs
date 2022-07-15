using System.Collections.Generic;
using System.Security.Claims;
using Castle.Core.Internal;
using Newtonsoft.Json;

namespace MEWC.API.Doamin.Module.Authorization
{
    public class ClaimsIdentityFactory
    {
        private readonly LoginTokenInfo _loginTokenInfo;

        public ClaimsIdentityFactory(LoginTokenInfo loginTokenInfo)
        {
            _loginTokenInfo = loginTokenInfo;
        }

        public ClaimsIdentity Create()
        {
            var claims = new List<Claim>();

            if (_loginTokenInfo == null)
                return new ClaimsIdentity(claims);

            //add total token
            AddClaim(claims, "loginTokenInfo", JsonConvert.SerializeObject(_loginTokenInfo));

            //add name
            AddClaim(claims, ClaimTypes.Name, _loginTokenInfo.LoginId);
            AddClaim(claims, ClaimTypes.GivenName, _loginTokenInfo.UserProfile?.Name);

            //add role
            if (_loginTokenInfo.Roles.Length>0)
            {
                foreach (Role role in _loginTokenInfo.Roles)
                {
                    AddClaim(claims, ClaimTypes.Role, role?.RoleName);
                }
            }

            return new ClaimsIdentity(claims);
        }

        private static void AddClaim(List<Claim> claims, string type, string value)
        {
            if (claims == null)
                return;

            if (!string.IsNullOrWhiteSpace(value))
                claims.Add(new Claim(type, value));
        }
    }
}
