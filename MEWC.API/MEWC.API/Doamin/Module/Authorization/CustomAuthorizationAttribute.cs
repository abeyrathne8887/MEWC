
using System;
using Microsoft.AspNetCore.Authorization;

namespace MEWC.API.Doamin.Module.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        public CustomAuthorizationAttribute() : base("CustomAuthorization")
        {
        }
    }
}