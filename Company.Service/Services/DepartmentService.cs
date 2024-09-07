using AutoMapper;

using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Dtos;
using Company.Service.Interface;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUnitOfWork _unitOfWork { get; }
        public IMapper _mapper { get; }

        public void Add(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }
        public DepartmentDto GetByIdAsNoTracking(int? id)
        {
            var department = _unitOfWork.DepartmentRepository.GetByIdAsNoTracking(id.Value);
            return _mapper.Map<DepartmentDto>(department);

        }
        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);


        }

        public DepartmentDto GetById(int? id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            return _mapper.Map<DepartmentDto>(department);
        }


        public void Update(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }

     
    }
}
