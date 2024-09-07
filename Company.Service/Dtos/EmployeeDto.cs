using Company.Data.Models;

using Microsoft.AspNetCore.Http;

namespace Company.Service.Dtos
{
    public class EmployeeDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNumer { get; set; }
        public DateTime HiringDate { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
