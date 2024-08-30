namespace EmployeeManagement.Migrations
{
    using EmployeeManagement.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManagement.Models.DataBaseContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeManagement.Models.DataBaseContext context)
        {
            // Verifica si la tabla está vacía antes de añadir datos
            if (!context.Employees.Any())
            {
                context.Employees.AddOrUpdate(
                    new Employee { Name = "Jhon", Position = "Manager", Office = "Medellin", Salary = 2000000 },
                    new Employee { Name = "Carmen", Position = "Developer", Office = "Medellin", Salary = 2500000 },
                    new Employee { Name = "Samuel", Position = "Designer", Office = "Medellin", Salary = 2000000 }
                );
            }
        }
    }
}
