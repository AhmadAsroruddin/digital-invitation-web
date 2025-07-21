using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;
using WebApi.Infrastructure.Identity;

namespace WebApi.Infrastructure.Data
{
    public class AppDbInitializer
    {
        public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            // 1. Sinkronisasi semua Role dari enum
            foreach (UserRole role in Enum.GetValues<UserRole>())
            {
                var roleName = role.ToString();
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Buat default admin user jika belum ada
            var superUsers = await userManager.GetUsersInRoleAsync(UserRole.SuperAdmin.ToString());
            if (superUsers.Count == 0)
            {
                const string adminEmail = "superadmin@mail.com";
                const string adminPassword = "@SuperAdmin123";

                var superUser = new ApplicationUser
                {
                    FullName = "Super Administrator",
                    Email = adminEmail,
                    UserName = adminEmail,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(superUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, UserRole.SuperAdmin.ToString());
                }
            }

            // 3. Seeder untuk Event dan SubEvent
            if (!context.Events.Any())
            {
                var mainEvent = new Event
                {
                    Name = "Novel & Sarah Wedding",
                    Date = new DateTime(2025, 8, 17),
                    Location = "Gedung Serbaguna",
                    Description = "Pernikahan Novel dan Sarah",
                    GroomName = "Novel",
                    BrideName = "Sarah",
                    GroomFamily = "Keluarga Novel",
                    BrideFamily = "Keluarga Sarah",
                    CreatedBy = "Seeder"
                };

                mainEvent.SubEvents = new List<SubEvent>
                {
                    new SubEvent
                    {
                        Name = "Akad Nikah",
                        StartTime = new DateTime(2025, 8, 17, 8, 0, 0),
                        EndTime = new DateTime(2025, 8, 17, 10, 0, 0),
                        Location = "Aula Utama",
                        MaxPax = 100
                    },
                    new SubEvent
                    {
                        Name = "Resepsi Siang",
                        StartTime = new DateTime(2025, 8, 17, 11, 0, 0),
                        EndTime = new DateTime(2025, 8, 17, 14, 0, 0),
                        Location = "Ballroom 1",
                        MaxPax = 300
                    },
                };

                context.Events.Add(mainEvent);
                await context.SaveChangesAsync();
            }

        }
    }
}