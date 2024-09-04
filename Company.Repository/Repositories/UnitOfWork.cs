using Company.Data.Contexts;
using Company.Repository.Interfaces;

namespace Company.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public CompanyDbContext _context { get; }

        public UnitOfWork(CompanyDbContext context)
        {
            DepartmentRepository = new DepartmentRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
            _context = context;
        }
        public int Complete()
        => _context.SaveChanges();
    }
}
