using Cyber1.Areas.Identity.Data;
using Cyber1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cyber1.Controllers
{
    public class UserListController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        public UserListController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public IActionResult GetUserList()
        {
            var users = _context.Users;

            var newlog = new LoggModel()
            {
                UserId = User.Identity.Name, //set the value, or set this column as auto increase (Identity).
                ActivityDate = DateTime.Now,
                Action = "Lista użytkowników",
                Description = "Wyświetlono liste użytkowników"
            };

            _context.Logs.Add(newlog); //insert into the database 
            _context.SaveChanges();  //save change.

            return View(users);
        }

    }
}
