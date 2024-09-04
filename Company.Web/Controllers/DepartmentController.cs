﻿using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Repository.Repositories;
using Company.Service.Interface;

using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            var department = _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            return Details(id, "Update");
        }
        [HttpPost]
        public IActionResult Update(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    _departmentService.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();

            }
            return Details(id, "Update");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);
            _departmentService.Delete(department);
            return RedirectToAction(nameof(Index));
        }

    }
}
