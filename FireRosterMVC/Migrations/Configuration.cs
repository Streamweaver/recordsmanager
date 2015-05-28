namespace FireRosterMVC.Migrations
{
    using FireRosterMVC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.Owin;
    using System.Web;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<FireRosterMVC.Models.FireRosterDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FireRosterMVC.Models.FireRosterDB context)
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
            context.Gender.AddOrUpdate(i => i.Label,
                new Gender
                {
                    Label = "Male"
                },
                new Gender
                {
                    Label = "Female"
                }
                );
            context.Race.AddOrUpdate(i => i.Label,
                new Race
                {
                    Label = "Asian"
                },
                new Race
                {
                    Label = "African American"
                },
                new Race
                {
                    Label = "Latino"
                },
                new Race
                {
                    Label = "Native American"
                },
                new Race
                {
                    Label = "Caucasian"
                }
                );

            InitializeIdentityForEF(context);

        }

        public static void InitializeIdentityForEF(FireRosterDB db)
        {
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));

            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }

            // Add other normal roles to app.
            List<String> roles = new List<string>();
            roles.Add("Payroll");
            roles.Add("RosterUser");
            roles.Add("CommandStaff");

            foreach (String title in roles)
            {
                if (roleManager.FindByName(title) == null)
                {
                    role = new ApplicationRole(roleName);
                    roleManager.Create(role);
                }
            }
        }
    }
}
