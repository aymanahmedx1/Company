using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interface;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork _unitOfWork { get; }

        public void Add(Department department)
        {
            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
          => _unitOfWork.DepartmentRepository.GetAll();

        public Department GetById(int? id)
        => _unitOfWork.DepartmentRepository.GetById(id.Value);

        public void Update(Department department)
        {
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
