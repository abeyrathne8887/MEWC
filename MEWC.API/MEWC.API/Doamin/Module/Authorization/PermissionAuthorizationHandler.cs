using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MEWC.API.Doamin.Module.Authorization
{
    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        //private readonly ISessionManagementAsyncService _sessionManagementService;
        private readonly ILogger _logger;

        public PermissionAuthorizationHandler(
          
            ILogger<PermissionAuthorizationHandler> logger)
        {
           // _sessionManagementService = sesssionManagementService;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, IEnumerable<PermissionAttribute> attributes)
        {
            foreach (var permissionAttribute in attributes)
            {
                //Authorize
                var loginTokenInfo = await Authorize(context.User);
                if (loginTokenInfo == null) return;


                //Check Permission
                if (!AuthorizePermission(loginTokenInfo, permissionAttribute.Module, permissionAttribute.Name))
                {
                    _logger.LogWarning($"[AUTHORIZATION] Permission for {permissionAttribute.Module}-{permissionAttribute.Name} is missing in token {context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value} of user {context.User.Identity.Name}");
                    return;
                }
            }

            context.Succeed(requirement);
            await Task.FromResult<object>(null);
        }

        private async Task<LoginTokenInfo> Authorize(ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (claim == null)
            {
                _logger.LogWarning($"[AUTHORIZATION] JWT Claim not found for user {user.Identity.Name}");
                return null;
            }

            var token = claim.Value;
            LoginTokenInfo userSession = null;// await _sessionManagementService.GetUserTokenByTokenAsync(token);

            if (userSession == null)
            {
                _logger.LogWarning($"[AUTHORIZATION] Login token info not found for {user.Identity.Name} using token {token}");
            }

            return userSession;
        }

        private bool AuthorizePermission(LoginTokenInfo loginTokenInfo, string module, string permission)
        {
            string[] permissions = permission.Split(',');
            return loginTokenInfo.Permissions.Any(p => p.ModuleName == module && permissions.Contains(p.PermissionName));
        }
    }
}
