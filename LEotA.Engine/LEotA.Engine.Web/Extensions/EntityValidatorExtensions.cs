﻿using Calabonga.Microservices.Core.Validators;
using System.Collections.Generic;

namespace LEotA.Engine.Web.Extensions
{
    /// <summary>
    /// Entity Validator Extensions
    /// </summary>
    public static class EntityValidatorExtensions
    {
        /// <summary>
        /// Returns validator from validation results
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ValidationContext GetResult(this List<ValidationResult> source) => new ValidationContext(source);
    }
}