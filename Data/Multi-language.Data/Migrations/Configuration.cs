namespace Multi_language.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
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
            if (!(context.Users.Any(u => u.UserName == "svetlin.krastanov90@gmail.com")))
            {
                var userStore = new UserStore<AppUser>(context);
                var userManager = new UserManager<AppUser>(userStore);
                var userToInsert = new AppUser { UserName = "svetlin.krastanov90@gmail.com", PhoneNumber = "0888017004", Email = "svetlin.krastanov90@gmail.com" };
                userManager.Create(userToInsert, "svetlin90");
            }
            if (!(context.Users.Any(u => u.UserName == "testuser@s2kdesign.com")))
            {
                var userStore = new UserStore<AppUser>(context);
                var userManager = new UserManager<AppUser>(userStore);
                var userToInsert = new AppUser { UserName = "testuser@s2kdesign.com", PhoneNumber = "0888017004", Email = "testuser@s2kdesign.com" };
                userManager.Create(userToInsert, "password");
            }
        }
    }
}
