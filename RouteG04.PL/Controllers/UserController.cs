using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RouteG04.BLL.DTOS.Department;
using RouteG04.BLL.DTOS.User;
using RouteG04.BLL.Services.Classes;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.Shared;
using RouteG04.PL.ViewModels.DepartmentViewModels;
using RouteG04.PL.ViewModels.UserViewModels;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace RouteG04.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IUserService userService, ILogger<UserController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IUserService _userService = userService;
        private readonly ILogger<UserController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;


        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string? UserSearchName)
        {
            List<ApplicationUser> users;
            if(string.IsNullOrEmpty(UserSearchName))
                users = await _userManager.Users.ToListAsync();
            else
                users = await _userManager.Users.
                    Where(user=>user.NormalizedEmail.Trim().Contains(UserSearchName.Trim().ToUpper())).ToListAsync();
            return View(users);
        }
        #endregion
        #region Details
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
           var user =await _userManager.FindByIdAsync(id);
            if(user == null) return NotFound();
            if(viewName == "Edit")
            {
                var userViewModel = new UserEditViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                return View(viewName,userViewModel);
            }
            return View(viewName,user);
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id,UserEditViewModel viewModel)
        {
            if(id != viewModel.Id) return NotFound();
            if(ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null) return NotFound();

                    user.UserName = viewModel.FirstName + "." + viewModel.LastName;
                    user.NormalizedUserName = user.UserName.ToUpper();
                    user.Email = viewModel.Email;
                    user.PhoneNumber = viewModel.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var item in result.Errors)
                    {
                        _logger.LogError(item.Description);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(viewModel);
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new UserViewModel
            {
                RolesList = await _roleManager.Roles
                    .Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    }).ToListAsync()
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.RolesList = await _roleManager.Roles
                    .Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    }).ToListAsync();

                return View(viewModel);
            }

            try
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    UserName = viewModel.Email,
                    NormalizedUserName = viewModel.Email.ToUpper(),
                    PhoneNumber = viewModel.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                    viewModel.RolesList = await _roleManager.Roles
                        .Select(r => new SelectListItem
                        {
                            Value = r.Name,
                            Text = r.Name
                        }).ToListAsync();

                    return View(viewModel);
                }

                if (!string.IsNullOrEmpty(viewModel.Roles))
                {
                    await _userManager.AddToRoleAsync(user, viewModel.Roles);
                }

                TempData["Message"] = "User Created Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("", ex.Message);
            }

            return View(viewModel);
        }

        #endregion



    }
}
