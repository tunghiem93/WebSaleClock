namespace CMS_Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CMS_Entity.CMS_Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CMS_Entity.CMS_Context context)
        {
            context.CMS_Employees.AddOrUpdate(x => x.Employee_Email, new Entity.CMS_Employee
            {
                Employee_Email = "admin@gmail.com",
                BirthDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                Employee_IDCard = "1234567890",
                Employee_Phone = "0975224063",
                FirstName = "David",
                Id = Guid.NewGuid().ToString(),
                IsActive = true,
                IsSupperAdmin = true,
                Password = "5bodbeBGJXU=",
                CreatedBy =Guid.NewGuid().ToString(),
                LastName = "Tuan",
                Employee_Address = "HCM",
                UpdatedDate = DateTime.Now,
            });
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
        }
    }
}
