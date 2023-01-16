using Cyber1.Areas.Identity.Data;
using Cyber1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cyber1.Controllers
{
    public class UserCrudController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserCrudController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public ViewResult Create() => View();

        //[Authorize]
        //[Authorize(Roles ="User")]
        [HttpPost]
        public async Task<IActionResult> Create(UserCrud userCrud)
        {
            // Logi
            var newlog = new LoggModel()
            {
                UserId = User.Identity.Name, //set the value, or set this column as auto increase (Identity).
                ActivityDate = DateTime.Now,
                Action = "Tworzenie użytkownika",
                Description = "Użytkownik został stworzony"
            };

            _context.Logs.Add(newlog); //insert into the database 
            _context.SaveChanges();  //save change.
            //logi 

            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    FirstName = userCrud.FirstName,
                    LastName = userCrud.LastName,
                    UserName = userCrud.UserName,
                    Email = userCrud.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, userCrud.Password);

                if (result.Succeeded)
                    return RedirectToAction("GetUserList", "UserList");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(userCrud);
        }
    }
}
