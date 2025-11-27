using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteG04.BLL.DTOS;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.PL.ViewModels.DepartmentViewModels;
using System.Diagnostics;

namespace RouteG04.PL.Controllers
{
    [Authorize]
    public class DepartmentsController(IDepartmentService deparmentService, ILogger<DepartmentsController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly IDepartmentService _deparmentService = deparmentService;
        private readonly ILogger<DepartmentsController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Message"] = new DepartmentsDto() { Name = "TestViewData" };
            ViewBag.Message = new DepartmentsDto() { Name = "TestViewData" };
            var Departments = _deparmentService.GetAllDepartments();
            return View(Departments);
        } 
        #endregion
        #region Create Department
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        DateOfCreation = viewModel.DateOfCreation,
                        Description = viewModel.Description
                    };

                    int Result = _deparmentService.AddDepartment(departmentDto);
                    string Message;
                    if (Result > 0)
                    {
                        Message = $"Department : {viewModel.Name} Has Been Created";
                    }
                    else
                    {
                        Message = $"Department : {viewModel.Name} Has Not Been Created";
                    }
                    TempData["Message"] =Message ;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //Log Exception
                    //Development
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);

                    }

                    //Deployment
                    else
                    {
                        _logger.LogError(ex.Message);

                    }
                }
            }

            return View(viewModel);

        }
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Department = _deparmentService.GetDepartmentById(id.Value);
            if (Department is null) return NotFound();
            return View(Department);

        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var Department = _deparmentService.GetDepartmentById(id.Value);
            if (Department is null) return NotFound();
            var DepartmentViewModel = new DepartmentViewModel
            {
                Code = Department.Code,
                Name = Department.Name,
                Description = Department.Description,
                DateOfCreation = Department.DateOfCreation
            };
            return View(DepartmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit ([FromRoute]int? id , DepartmentViewModel viewModels)
        {
            if (!id.HasValue) return View(viewModels);
            try
            {
                var UpdatedDepartment = new UpdatedDepartmentDto()
                {
                    Id = id.Value,
                    Code = viewModels.Code,
                    Name = viewModels.Name,
                    Description = viewModels.Description,
                    DateOfCreation = viewModels.DateOfCreation
                };

                int Result = _deparmentService.UpdateDepartment(UpdatedDepartment);

                if(Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can Not Be Updated");
                    return View(viewModels);

                }
            }
            catch (Exception ex)
            {
                //Log Exception
                //Development
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModels);
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
        //[HttpGet ]
        //public IActionResult Delete(int? id)
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var Department = _deparmentService.GetDepartmentById(id.Value);
        //    if (Department is null) return NotFound();
        //    return View(Department);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            {
                bool IsDeleted = _deparmentService.DeleteDepartment(id);
                if (IsDeleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Is Not Deleted");
                    return RedirectToAction(nameof(Index), new { id });
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
                    return View("ErrorView",ex);
                }
            }
        }
        #endregion
    }
}
