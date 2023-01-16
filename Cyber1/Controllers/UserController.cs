using Cyber1.Areas.Identity.Data;
using Cyber1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyber1.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //[Authorize]
        //[Authorize(Roles ="User")]
        // GET: UserController/Edit/5
        public IActionResult ChangePassword()
        {
            return View();
        }
        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "User")]

        public async Task<IActionResult> ChangePassword(string Password, string id)
        {
            //logi
            var newlog = new LoggModel()
            {
                UserId = User.Identity.Name, //set the value, or set this column as auto increase (Identity).
                ActivityDate = DateTime.Now,
                Action = "Zmiana hasła",
                Description = "Zmieniono hasło użytkownikowi"
            };

            _context.Logs.Add(newlog); //insert into the database 
            _context.SaveChanges();  //save change.
            //logi

            var user =  await _userManager.FindByIdAsync(id);

            _userManager.RemovePasswordAsync(user);

            _userManager.AddPasswordAsync(user, Password);

            await _userManager.UpdateAsync(user);

            return RedirectToAction("GetUserList", "UserList");
        }

    }
}
