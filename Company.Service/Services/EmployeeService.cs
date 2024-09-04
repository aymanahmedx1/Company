using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interface;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork _unitOfWork { get; }

        public void Add(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
        => _unitOfWork.EmployeeRepository.GetAll();

        public Employee GetById(int? id)
        => _unitOfWork.EmployeeRepository.GetById(id.Value);

        public void Update(Employee employee)
        {
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
