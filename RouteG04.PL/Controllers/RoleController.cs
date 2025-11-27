using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteG04.BLL.DTOS.Role;
using RouteG04.BLL.DTOS.User;
using RouteG04.BLL.Services.Classes;
using RouteG04.BLL.Services.Interfaces;
using RouteG04.DAL.Models.Shared;
using RouteG04.PL.ViewModels.RoleViewModels;
using RouteG04.PL.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace RouteG04.PL.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController( RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager,ILogger<RoleController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ILogger<RoleController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string? RoleSearchName)
        {
            IQueryable<IdentityRole> rolesQuery = _roleManager.Roles;

            if (!string.IsNullOrEmpty(RoleSearchName))
            {
                rolesQuery = rolesQuery
                    .Where(r => r.NormalizedName.Contains(RoleSearchName.Trim().ToUpper()));
            }

            var roles = await rolesQuery.ToListAsync();

            return View(roles);
        }
        #endregion
        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = viewModel.Name
                };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                }
            }
                return View(viewModel);
        }
        #endregion
        #region Details
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
           
                var roleViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };

            return View(viewName, roleViewModel);
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role == null) return NotFound();

                    role.Name = viewModel.Name;
                    role.NormalizedName = viewModel.Name.ToUpper();
                   

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role Updated Successfully");
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
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound();

                var result = await _roleManager.DeleteAsync(role);

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
        #region AddOrRemoveUsers
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return NotFound();

            ViewBag.RoleId = roleId;

            var users = await _userManager.Users.ToListAsync();

            var usersInRole = new List<UserInRoleViewModel>();

            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userInRole.IsSelected = true;
                else
                    userInRole.IsSelected = false;
                
                usersInRole.Add(userInRole);
            }
            return View(usersInRole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> usersInRole)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            

            if(ModelState.IsValid)
            {
                foreach (var user in usersInRole)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if(appUser is not null)
                    {
                        if(user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction("Edit", new {id = roleId});
            }
            return View(usersInRole);
        }
        #endregion

    }
}
