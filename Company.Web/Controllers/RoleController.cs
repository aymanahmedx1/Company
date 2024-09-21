using Company.Data.Models;
using Company.Service.Dtos;
using Company.Web.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles ="Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole input)
        {
            if (ModelState.IsValid)
            {
                var res = await _roleManager.CreateAsync(input);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            var user = await _roleManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            if (viewName == "Update")
            {
                var userviewModel = new UpdateRoleViewModel
                {
                    Id = user.Id,
                    Name = user.Name
                };

                return View(viewName, userviewModel);
            }
            return View(viewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateRoleViewModel input)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(input.Id);
                if (role is not null)
                {
                    role.Name = input.Name;
                    role.NormalizedName = input.Name.ToUpper();
                    var res = await _roleManager.UpdateAsync(role);
                    if (res.Succeeded)
                    {

                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return await Update(input.Id);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is not null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();
            var UsersInRoll = new List<UserInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userViewModel = new UserInRoleViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userViewModel.IsChecked = true;
                else
                    userViewModel.IsChecked = false;
                UsersInRoll.Add(userViewModel);
            }
            ViewBag.role = roleId;

            return View(UsersInRoll);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> userList)
        {

            var identityRole = await _roleManager.FindByIdAsync(roleId);
            if (identityRole is null)
                return NotFound();
            foreach (var user in userList)
            {
                var appUser = await _userManager.FindByIdAsync(user.Id);
                if (appUser is not null)
                {
                    if (user.IsChecked && !await _userManager.IsInRoleAsync(appUser, identityRole.Name))
                    {
                        await _userManager.AddToRoleAsync(appUser, identityRole.Name);
                    }
                    else if (!user.IsChecked && await _userManager.IsInRoleAsync(appUser, identityRole.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(appUser, identityRole.Name);
                    }
                }

            }
            return RedirectToAction("Update", new { id = roleId });
        }
    }
}
