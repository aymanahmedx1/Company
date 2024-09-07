using Company.Service.Dtos;
using Company.Service.Interface;

using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IEmployeeService EmployeeService, IDepartmentService departmentService)
        {
            _EmployeeService = EmployeeService;
            _departmentService = departmentService;
        }

        public IEmployeeService _EmployeeService { get; }
        public IDepartmentService _departmentService { get; }

        public IActionResult Index(string searchText)
        {
            IEnumerable<EmployeeDto> employeeDtos = new List<EmployeeDto>();

            if (!string.IsNullOrEmpty(searchText))
            {
                employeeDtos = _EmployeeService.GetByName(searchText);
                ViewBag.SearchText = searchText;
            }
            else
                employeeDtos = _EmployeeService.GetAll();

            return View(employeeDtos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto EmployeeDto)
        {
            if (ModelState.IsValid)
            {
                _EmployeeService.Add(EmployeeDto);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            var EmployeeDto = _EmployeeService.GetById(id);
            if (EmployeeDto == null)
            {
                return NotFound();
            }
            ViewBag.Departments = _departmentService.GetAll();

            return View(viewName, EmployeeDto);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            ViewBag.error = true;
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int id, EmployeeDto EmployeeDto)
        {
            if (ModelState.IsValid)
            {
                if (id == EmployeeDto.Id)
                {
                    _EmployeeService.Update(EmployeeDto);
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();

            }
            return Details(id, "Update");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var EmployeeDto = _EmployeeService.GetByIdAsNoTracking(id);
            _EmployeeService.Delete(EmployeeDto);
            return RedirectToAction(nameof(Index));
        }



    }
}
