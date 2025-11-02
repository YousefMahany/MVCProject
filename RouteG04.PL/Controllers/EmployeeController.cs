using Microsoft.AspNetCore.Mvc;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.DTOS.Employee;
using RouteG04.BLL.Services.Classes;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.EmployeeModule;
using RouteG04.PL.ViewModels.DepartmentViewModels;

namespace RouteG04.PL.Controllers
{
    public class EmployeeController(IEmployeeService employeeService, IWebHostEnvironment environment,ILogger<EmployeeController> logger) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ILogger _logger = logger;

        #region Index
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees(false);
            return View(Employees);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
            return View(employeeDto);
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
            var EmployeeDto = new UpdatedEmployeeDto
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Salary = Employee.Salary,
                Address = Employee.Address,
                Age = Employee.Age,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                IsActive = Employee.IsActive,
                HiringDate = Employee.HiringDate,
                Gender = Employee.EmpGender,
                EmployeeType = Employee.EmployeeType
            };
            return View(EmployeeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, UpdatedEmployeeDto employeeDto)
        {
            if (!id.HasValue) return View(employeeDto);
            try
            {

                int Result = _employeeService.UpdateEmployee(employeeDto);

                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Can Not Be Updated");
                    return View(employeeDto);

                }
            }
            catch (Exception ex)
            {
                //Log Exception
                //Development
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeDto);
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
