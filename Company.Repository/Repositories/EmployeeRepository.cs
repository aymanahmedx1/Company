using Company.Data.Contexts;
using Company.Data.Models;
using Company.Repository.Interfaces;

namespace Company.Repository.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }
        public CompanyDbContext _context { get; }

        public IEnumerable<Employee> GetByName(string name)
        => _context.Employees.Where(x => x.Name.Contains(name));
    }
}
