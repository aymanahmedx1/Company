

using Company.Data.Models;

namespace Company.Service.Interface
{
    public interface IDepartmentService
    {
        public void Add(Department department);
        public void Update(Department department);
        public void Delete(Department department);
        public Department GetById(int? id);
        public IEnumerable<Department> GetAll();
    }
}
