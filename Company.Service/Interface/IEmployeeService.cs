using Company.Service.Dtos;

namespace Company.Service.Interface
{
    public interface IEmployeeService
    {
        public void Add(EmployeeDto EmployeeDto);
        public void Update(EmployeeDto EmployeeDto);
        public void Delete(EmployeeDto EmployeeDto);
        public EmployeeDto GetById(int? id);
        public EmployeeDto GetByIdAsNoTracking(int? id);
        public IEnumerable<EmployeeDto> GetByName(string name);
        public IEnumerable<EmployeeDto> GetAll();
    }
}
