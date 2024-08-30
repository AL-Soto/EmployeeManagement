using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("name=MyDatabaseConnectionString")
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}