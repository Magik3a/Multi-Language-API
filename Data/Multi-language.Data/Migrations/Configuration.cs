using Multi_language.Common.Enums;

namespace Multi_language.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Common.Helpers;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Multi_language.Data.MultiLanguageDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Multi_language.Data.MultiLanguageDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            foreach (var roleEnum in Enum.GetValues(typeof(ERoleLevels)))
            {
                if (!roleManager.RoleExists(roleEnum.ToString()))
                {
                    var roleToInsert = new ApplicationRole()
                    {
                        Name = roleEnum.ToString(),
                        Level = (Int16)roleEnum.GetHashCode()
                    };
                    roleManager.Create(roleToInsert);
                }
            }
            if (!(context.Users.Any(u => u.UserName == "svetlin.krastanov90@gmail.com")))
            {
                var userStore = new UserStore<AppUser>(context);
                var userManager = new UserManager<AppUser>(userStore);
                var userToInsert = new AppUser
                {
                    UserName = "svetlin.krastanov90@gmail.com",
                    PhoneNumber = "0888017004",
                    Email = "svetlin.krastanov90@gmail.com"
                };
                userManager.Create(userToInsert, "svetlin90");
                userManager.AddToRole(userToInsert.Id, ERoleLevels.AdminPermissions.ToString());

            }
            if (!(context.Users.Any(u => u.UserName == "testuser@s2kdesign.com")))
            {
                var userStore = new UserStore<AppUser>(context);
                var userManager = new UserManager<AppUser>(userStore);
                var userToInsert = new AppUser {
                    UserName = "testuser@s2kdesign.com",
                    PhoneNumber = "0888017004",
                    Email = "testuser@s2kdesign.com"
                };
                userManager.Create(userToInsert, "password");
                userManager.AddToRole(userToInsert.Id, ERoleLevels.UserPermissions.ToString());

            }

            if (!context.Clients.Any())
            {
                context.Clients.Add(new Client()
                {
                    Id = "ngAuthApp",
                    Secret = Helper.GetHash("abc@123"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = Models.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "http://localhost:32150"

                });
            }
        }
    }
}
