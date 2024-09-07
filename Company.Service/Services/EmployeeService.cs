using AutoMapper;

using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dtos;
using Company.Service.Helpers;
using Company.Service.Interface;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper
         )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public void Add(EmployeeDto employeeDto)
        {
            employeeDto.ImageUrl = DocumentHelper.UploadFile(employeeDto.Image, "employees");
            var employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            DocumentHelper.DeleteFile(employee.ImageUrl, "employees");
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public EmployeeDto GetById(int? id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            return _mapper.Map<EmployeeDto>(employee);

        }
        public EmployeeDto GetByIdAsNoTracking(int? id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetByIdAsNoTracking(id.Value);
            return _mapper.Map<EmployeeDto>(employee);

        }

        public IEnumerable<EmployeeDto> GetByName(string name)
        {
            var employees = _unitOfWork.EmployeeRepository.GetByName(name);
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public void Update(EmployeeDto employeeDto)
        {
            if (employeeDto.Image != null)
            {
                employeeDto.ImageUrl = DocumentHelper.UploadFile(employeeDto.Image, "employees");
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
