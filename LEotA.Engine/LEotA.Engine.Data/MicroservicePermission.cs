﻿using Calabonga.EntityFrameworkCore.Entities.Base;
using System;

namespace LEotA.Engine.Data
{
    /// <summary>
    /// User permission for microservice
    /// </summary>
    public class MicroservicePermission : Auditable
    {
        /// <summary>
        /// Application User profile identifier
        /// </summary>
        public Guid ApplicationUserProfileId { get; set; }

        /// <summary>
        /// Application User Profile
        /// </summary>
        public virtual ApplicationUserProfile ApplicationUserProfile { get; set; }

        /// <summary>
        /// Authorize attribute policy name
        /// </summary>
        public string PolicyName { get; set; }

        /// <summary>
        /// Description for current permission
        /// </summary>
        public string Description { get; set; }
    }
}