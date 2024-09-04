using Company.Data.Models;
using Company.Service.Interface;
using Company.Service.Services;

using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeService employeeService , IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        public IEmployeeService _employeeService { get; }
        public IDepartmentService _departmentService { get; }

        public IActionResult Index()
        {
            var emps = _employeeService.GetAll();
            return View(emps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageUrl = "";
                _employeeService.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Departments = _departmentService.GetAll();
            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id == employee.Id)
                {
                    _employeeService.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();

            }
            return Details(id, "Update");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetById(id);
            _employeeService.Delete(employee);
            return RedirectToAction(nameof(Index));
        }



    }
}
