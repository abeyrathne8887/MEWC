using System;
using Microsoft.AspNetCore.Authorization;

namespace MEWC.API.Doamin.Module.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Module { get; }
        public string Name { get; }

        public PermissionAttribute(string module, string name) : base("Permission")
        {
            Module = module;
            Name = name;
        }
    }
}
