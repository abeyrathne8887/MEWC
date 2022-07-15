
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MEWC.API.Doamin.Module.Authorization
{
    public class CustomAuthorizeHandler : CustomAttributeAuthorizationHandler<CustomAuthorizationRequirement, CustomAuthorizationAttribute>
    {
        //private readonly ISessionManagementAsyncService _sessionManagementService;

        public CustomAuthorizeHandler()
        {
           //_sessionManagementService = sesssionManagementService;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomAuthorizationRequirement requirement,
            IEnumerable<CustomAuthorizationAttribute> attributes)
        {
            bool hasAccessTokenExpired = false;
            hasAccessTokenExpired = HasAccessTokenExpired(context);

            var loginTokenInfo = hasAccessTokenExpired ? null : await Authorize(context?.User);
            if (loginTokenInfo == null)
            {
                context?.Fail();

                var filterContext = context.Resource as AuthorizationFilterContext;
                var response = filterContext?.HttpContext.Response;
                response?.OnStarting(async () =>
                {
                    filterContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                });
            }
            else
            {
                if (context?.Resource != null)
                {
                    var authHeaderString = ((AuthorizationFilterContext)context.Resource).
                        HttpContext.Request.Headers["Authorization"].ToString();
                    loginTokenInfo.AccessToken = authHeaderString.Split(' ')[1];
                }
                AddLoginInfoToClaim(context, loginTokenInfo);
                context?.Succeed(requirement);
            }

            //return Task.CompletedTask;
        }

        private bool HasAccessTokenExpired(AuthorizationHandlerContext context)
        {
            string accessToken = string.Empty;
            bool isTokenExpired = false;
            if (context?.Resource != null)
            {
                var authHeaderString = ((AuthorizationFilterContext)context.Resource).
                    HttpContext.Request.Headers["Authorization"].ToString();
                accessToken = authHeaderString.Split(' ')[1];
            }

            var jwthandler = new JwtSecurityTokenHandler();
            var jwttoken = jwthandler.ReadToken(accessToken);
            var expDate = jwttoken.ValidTo;
            if (expDate < DateTime.UtcNow.AddMinutes(1))
                isTokenExpired = true;

            return isTokenExpired;
        }

        private async Task<LoginTokenInfo> Authorize(ClaimsPrincipal user)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (claim == null)
                return null;

            var token = claim.Value;
            LoginTokenInfo userSession = null; // await _sessionManagementService.GetUserTokenByTokenAsync(token);
            if (userSession != null && !userSession.Valid)
                return null;
            return userSession;
        }

        protected void AddLoginInfoToClaim(AuthorizationHandlerContext context, LoginTokenInfo loginTokenInfo)
        {
            ////Add custom claim for userSession
            //ClaimsIdentityFactory claimsIdentityFactory = new ClaimsIdentityFactory(loginTokenInfo);
            //class that add logininfo to 

            //var appIdentity = claimsIdentityFactory.Create();

            //context.User.AddIdentity(appIdentity);

            //Thread.CurrentPrincipal = new ClaimsPrincipal(appIdentity);
        }
    }
}
