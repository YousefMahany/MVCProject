using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.Services.Classes;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.PL.ViewModels.DepartmentViewModels;
using RouteG04.PL.ViewModels.EmployeeViewModels;

namespace RouteG04.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService, IWebHostEnvironment environment,ILogger<EmployeeController> logger,IDepartmentService departmentService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ILogger _logger = logger;
        private readonly IDepartmentService _departmentService = departmentService;

        #region Index
        public IActionResult Index( string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(false);
            if(!string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                Employees = Employees.Where(e=>e.Name.Contains(EmployeeSearchName,StringComparison.OrdinalIgnoreCase));
            }
            return View(Employees);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var Model = new EmployeeViewModel
            {
                Departments = _departmentService.GetAllDepartments()
                .Select(d => new SelectListItem { Value = d.DeptId.ToString(), Text = d.Name })
            };
            return View(Model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto
                    {
                        Name = employeeVM.Name,
                        Age = employeeVM.Age,
                        Address = employeeVM.Address,
                        Email = employeeVM.Email,
                        EmployeeType = employeeVM.EmployeeType,
                        Gender = employeeVM.Gender,
                        HiringDate = employeeVM.HiringDate,
                        PhoneNumber = employeeVM.PhoneNumber,
                        Salary = employeeVM.Salary,
                        DepartmentId = employeeVM.DepartmentId,
                        IsActive = employeeVM.IsActive,
                        Image = employeeVM.Image

                    };
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if(Result>0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                         ModelState.AddModelError(string.Empty, "Can Not Create Employee");
                    }
                }

                catch (Exception ex)
                {
                    //Log Exception
                    //Development
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return RedirectToAction(nameof(Index));
                    }

                    //Deployment
                    else
                    {
                        _logger.LogError(ex.Message);
                        return View("ErrorView", ex);
                    }
                }

            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e=>e.Errors).Select(e=>e.ErrorMessage)
                    .ToList();
                ViewBag.Errors = errors;    
                employeeVM.Departments = _departmentService.GetAllDepartments()
                .Select(d => new SelectListItem { Value = d.DeptId.ToString(), Text = d.Name });
            }
            return View(employeeVM);
        }
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Employee = _employeeService.GetEmployeeId(id.Value);
            if (Employee is null) return NotFound();
            return View(Employee);

        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Employee = _employeeService.GetEmployeeId(id.Value);
            if (Employee is null) return NotFound();
            var EmployeeVM = new EmployeeViewModel
            {
                
                Name = Employee.Name,
                Salary = Employee.Salary,
                Address = Employee.Address,
                Age = Employee.Age,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                IsActive = Employee.IsActive,
                HiringDate = Employee.HiringDate,
                Gender = Employee.EmpGender,
                EmployeeType = Employee.EmployeeType,
                DepartmentId = Employee.DepartmentId
            };
            EmployeeVM.Departments = _departmentService.GetAllDepartments()
                .Select(d => new SelectListItem
                {
                    Value = d.DeptId.ToString(),
                    Text = d.Name,
                    Selected = d.DeptId == Employee.DepartmentId
                });
            return View(EmployeeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel EmployeeVM)
        {
            if (!id.HasValue) return BadRequest();
            
            if (!ModelState.IsValid) 
            {
                EmployeeVM.Departments = _departmentService.GetAllDepartments()
                .Select(d => new SelectListItem
                {
                    Value = d.DeptId.ToString(),
                    Text = d.Name,
                    Selected = d.DeptId == EmployeeVM.DepartmentId
                });
                return View(EmployeeVM);
            }
            try
            {
                var employeeDto = new UpdatedEmployeeDto
                {
                    Id = id.Value,
                    Name = EmployeeVM.Name,
                    Salary = EmployeeVM.Salary,
                    Address = EmployeeVM.Address,
                    Age = EmployeeVM.Age,
                    Email = EmployeeVM.Email,
                    PhoneNumber = EmployeeVM.PhoneNumber,
                    IsActive = EmployeeVM.IsActive,
                    HiringDate = EmployeeVM.HiringDate,
                    Gender = EmployeeVM.Gender,
                    EmployeeType = EmployeeVM.EmployeeType,
                    DepartmentId = EmployeeVM.DepartmentId
                };
                var Result = _employeeService.UpdateEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Employee Can Not Be Updated");
                EmployeeVM.Departments = _departmentService.GetAllDepartments()
               .Select(d => new SelectListItem
               {
                   Value = d.DeptId.ToString(),
                   Text = d.Name,
                   Selected = d.DeptId == EmployeeVM.DepartmentId
               });
                return View(EmployeeVM);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                    EmployeeVM.Departments = _departmentService.GetAllDepartments()
                   .Select(d => new SelectListItem
                   {
                       Value = d.DeptId.ToString(),
                       Text = d.Name,
                       Selected = d.DeptId == EmployeeVM.DepartmentId
                   });
                    return View(EmployeeVM);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView");
                }
            }
            

        }
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var IsDeleted = _employeeService.DeleteEmployee(id);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can Not Be Deleted");
                    return RedirectToAction(nameof(Delete), new { id = id });
                }
            }
            catch (Exception ex)
            {
                //Log Exception
                //Development
                if (_environment.IsDevelopment())
                {
                   // ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }

                //Deployment
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion
    }
}
