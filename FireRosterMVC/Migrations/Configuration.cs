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
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(db));

            Dictionary<string, string> rolesData = new Dictionary<string, string>();
            rolesData.Add("Admin", "Super users, can do anything.");
            rolesData.Add("Payroll", "All operations for information related to position and pay");
            rolesData.Add("Manager", "All operations for staff information, scheduling and Locations.");
            foreach (KeyValuePair<string, string> roleData in rolesData)
            {
                if (roleManager.FindByName(roleData.Key) == null)
                {
                    ApplicationRole role = new ApplicationRole(roleData.Key);
                    role.Description = roleData.Value;
                    roleManager.Create(role);
                }
            }

            const string name = "admin@example.com";
            const string password = "Admin@123456";  // IMPORTANT CHANGE THIS after first login.
            ApplicationRole adminRole = roleManager.FindByName("Admin");

            var user = userManager.FindByName(name);
            if (db.Users.Count() < 1) // Create initial admin user if no users in DB.
            {
                user = new ApplicationUser { UserName = name, Email = name };
                userManager.Create(user, password);
                userManager.SetLockoutEnabled(user.Id, false);
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(adminRole.Name))
                {
                    userManager.AddToRole(user.Id, adminRole.Name);
                }
            }
        }
    }
}
