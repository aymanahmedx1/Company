using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Company.Data.Models;
using Company.Service.Dtos;

namespace Company.Service.Helpers
{
    public class CustomAutoMapper : Profile
    {
        public CustomAutoMapper()
        {
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<DepartmentDto, Department>().ReverseMap();
        }
    }
}
