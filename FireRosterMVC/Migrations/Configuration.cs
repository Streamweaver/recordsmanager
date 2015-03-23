namespace FireRosterMVC.Migrations
{
    using FireRosterMVC.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

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
        }
    }
}
