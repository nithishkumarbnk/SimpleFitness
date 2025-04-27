using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Required for LINQ methods
using System.Security.Claims;
using WebApplication1.Data; // Required for ClaimTypes
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly FitnessDbContext _context; // Your database context

        public UserController(FitnessDbContext context) // Constructor injection
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var loggedInUserIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(loggedInUserIdString, out int loggedInUserId))
            {
                var users = _context.Users.Where(u => u.Id == loggedInUserId).ToList();

                return View(users); // Pass the list of users directly to the view
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid user ID.");
                return View(new List<User>()); // Return an empty list or handle appropriately
            }
        }

    }
}
