

using Company.Data.Models;
using Company.Service.Dtos;

namespace Company.Service.Interface
{
    public interface IDepartmentService
    {
        public void Add(DepartmentDto department);
        public void Update(DepartmentDto department);
        public void Delete(DepartmentDto department);
        public DepartmentDto GetById(int? id);
        public DepartmentDto GetByIdAsNoTracking(int? id);
        public IEnumerable<DepartmentDto> GetAll();
    }
}
