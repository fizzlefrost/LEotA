﻿using LEotA.Engine.Entities.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LEotA.Engine.Data.DatabaseInitialization
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        public static async void Seed(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            // Не успевает запуститься
            await Task.Delay(5000);

            // Should be uncomment when using UseSqlServer() settings or any other provider.
            // This is should not be used when UseInMemoryDatabase()
            if ((await context.Database.GetPendingMigrationsAsync()).Any())
            {
                await context.Database.MigrateAsync();
            }

            var roles = AppData.Roles.ToArray();

            foreach (var role in roles)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role, NormalizedName = role.ToUpper() });
                }
            }

            #region developer

            var developer1 = new ApplicationUser
            {
                Email = "leonidimeev@yandex.ru",
                NormalizedEmail = "LEONIDIMEEV@YANDEX.RU",
                UserName = "leonidimeev@yandex.ru",
                FirstName = "Leonid",
                LastName = "Imeev",
                PatronomicName = "Vladimirovitch",
                EmbedLink = "https://www.google.ru",
                NormalizedUserName = "LEONIDIMEEV@YANDEX.RU",
                PhoneNumber = "+79142650809",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ApplicationUserProfile = new ApplicationUserProfile
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = "SEED",
                    Permissions = new List<MicroservicePermission>
                    {
                        new MicroservicePermission
                        {
                            CreatedAt = DateTime.Now,
                            CreatedBy = "SEED",
                            PolicyName = "Logs:UserRoles:View",
                            Description = "Access policy for Logs controller user view"
                        }
                    }
                }
            };

            if (!context.Users.Any(u => u.UserName == developer1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(developer1, "Qwerty123!");
                developer1.PasswordHash = hashed;
                var userStore = scope.ServiceProvider.GetService<ApplicationUserStore>();
                var result = await userStore.CreateAsync(developer1);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Cannot create account");
                }

                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                foreach (var role in roles)
                {
                    var roleAdded = await userManager.AddToRoleAsync(developer1, role);
                    if (roleAdded.Succeeded)
                    {
                        await context.SaveChangesAsync();
                    }
                }
            }
            #endregion

            await context.SaveChangesAsync();
        }
    }
}