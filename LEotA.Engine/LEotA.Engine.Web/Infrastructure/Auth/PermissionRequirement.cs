﻿using Microsoft.AspNetCore.Authorization;

namespace LEotA.Engine.Web.Infrastructure.Auth
{
    /// <summary>
    /// Permission requirement for user or service authorization
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permissionName) => PermissionName = permissionName;

        /// <summary>
        /// Permission name
        /// </summary>
        public string PermissionName { get; }
    }
}