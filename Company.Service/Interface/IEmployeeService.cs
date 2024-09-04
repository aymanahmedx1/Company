using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Company.Data.Models;

namespace Company.Service.Interface
{
    public interface IEmployeeService
    {
        public void Add(Employee employee);
        public void Update(Employee employee);
        public void Delete(Employee employee);
        public Employee GetById(int? id);
        public IEnumerable<Employee> GetAll();
    }
}
