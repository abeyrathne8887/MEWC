using System.Linq;
using System.Security.Claims;
using MEWC.API.Doamin.Module;
using MEWC.API.Doamin.Module.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MEWC.API.Doamin.Extensions
{
    public static class AuthorizationExtensions
    {
        private const string LoginTokenInfo = "loginTokenInfo";

        public static void AddCustomAuthorization(this IServiceCollection services, bool? useStub = false)
        {

            SetCustomAuthorization(services);

            SetLoginTokenInfo(services);
        }

        private static void SetLoginTokenInfo(IServiceCollection services)
        {
            services.AddTransient(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                ClaimsPrincipal user = accessor.HttpContext.User;
                //TODO: FIX LATER (PHILIP)
                Claim loginTokenClaim = user.Claims.FirstOrDefault(c => c.Type == LoginTokenInfo);

                if (loginTokenClaim == null)
                    return new LoginTokenInfo();  // Avoid activation failure when [AllowAnonymous] attribute is applied at controller level (Should not happen)

                // In this case a valid claim for loginTokenInfo exists
                // It should be stored as a JSON representation of LoginTokenInfo
                var loginTokenInfo = JsonConvert.DeserializeObject<LoginTokenInfo>(loginTokenClaim.Value);
                return loginTokenInfo;
            });
        }

        private static void SetCustomAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CustomAuthorization",
                    policyBuilder => policyBuilder.AddRequirements(
                        new CustomAuthorizationRequirement()));

                options.AddPolicy(
                    "Permission",
                    policyBuilder => policyBuilder.AddRequirements(
                        new PermissionAuthorizationRequirement()));
            });

            services.AddTransient<IAuthorizationHandler, CustomAuthorizeHandler>();
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }


    }
}
