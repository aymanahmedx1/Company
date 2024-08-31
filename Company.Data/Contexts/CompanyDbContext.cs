using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Company.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Company.Data.Contexts
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
