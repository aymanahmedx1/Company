using Company.Data.Models;
using Company.Service.Dtos;
using Company.Web.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string searchKeyword)
        {
            IEnumerable<AppUser> users;
            if (string.IsNullOrEmpty(searchKeyword))
                users = await _userManager.Users.ToListAsync();
            else
            {
                users = await _userManager.Users
                    .Where(user => user.Email.Trim().ToLower()
                    .Contains(searchKeyword.Trim().ToLower()))
                    .ToListAsync();
                ViewBag.SearchText = searchKeyword;
            }

            return View(users);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            if (viewName == "Update")
            {
                var userviewModel = new UpdateUserViewModel
                {
                    Email = user.Email,
                    Id = user.Id,
                    UserName = user.UserName
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
        public async Task<IActionResult> Update(UpdateUserViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(input.Id);
                if (user is not null)
                {
                    user.UserName = input.UserName;
                    user.NormalizedUserName = input.UserName.ToUpper();
                    var res = await _userManager.UpdateAsync(user);
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
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null) { 
                user.IsDeleted = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

    }
}
