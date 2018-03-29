namespace SlowNewsBlog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SlowNewsBlog.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SlowNewsBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SlowNewsBlog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            string adminRole = "Admin";
            IdentityResult roleResult;

            if (!RoleManager.RoleExists(adminRole))
            {
                roleResult = RoleManager.Create(new IdentityRole(adminRole));
            }

            string bloggerRole = "Blogger";
            IdentityResult identityResult;

            if (!RoleManager.RoleExists(bloggerRole))
            {
                identityResult = RoleManager.Create(new IdentityRole(bloggerRole));
            }

            if(!context.Users.Any(u => u.UserName == "bill@bill.com"))
            {
                var user = new ApplicationUser()
                {
                    UserName = "bill@bill.com",
                    Email = "bill@bill.com"
                };

                UserManager.Create(user, "twofarts");

                UserManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "phil@phil.com"))
            {
                var user = new ApplicationUser()
                {
                    UserName = "phil@phil.com",
                    Email = "phil@phil.com"
                };

                UserManager.Create(user, "threefarts");

                UserManager.AddToRole(user.Id, "Blogger");
            }

        }
    }
}
