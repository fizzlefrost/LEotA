﻿using Microsoft.Extensions.Logging;

using System;

namespace LEotA.Engine.Web.Infrastructure.EventLogs
{
    /// <summary>
    /// Event logging as ILogger extension.
    /// Please see the video as the manual https://youtu.be/09EVKgHgwnM
    /// </summary>
    internal static class LoggerExtensions
    {
        #region UserRegistration

        /// <summary>
        /// Log register action event
        /// </summary>
        /// <param name="source"></param>
        /// <param name="userName"></param>
        /// <param name="exception"></param>
        internal static void MicroserviceUserRegistration(this ILogger source, string userName, Exception? exception = null)
        {
            switch (exception)
            {
                case null:
                    UserRegistrationExecute(source, userName, exception);
                    break;
                default:
                    UserRegistrationFailedExecute(source, userName, exception);
                    break;
            }
        }

        private static readonly Action<ILogger, string, Exception?> UserRegistrationExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventNumbers.UserRegistrationId,
                "User {userName} successfully registred");

        private static readonly Action<ILogger, string, Exception?> UserRegistrationFailedExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventNumbers.UserRegistrationId,
                "User {userName} registred failed");

        #endregion

        #region PostItem

        internal static void MicroservicePostItem(this ILogger source, string entityName, Exception? exception = null)
        {
            switch (exception)
            {
                case null:
                    PostItemExecute(source, entityName, null);
                    break;

                default:
                    PostItemFailedExecute(source, entityName, exception);
                    break;
            }
        }

        private static readonly Action<ILogger, string, Exception?> PostItemExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventNumbers.PostItemId,
                "The {entityName} successfully saved");

        private static readonly Action<ILogger, string, Exception?> PostItemFailedExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventNumbers.PostItemId,
                "The {entityName} saving failed");

        #endregion
    }
}
