using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly FitnessDbContext _context;

        public UsersController(FitnessDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Email,Age,Height,Gender,Weight,Role,Image")] User user)
        {
            // Validate that Password is not null or empty
            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "Password is required.");
                return View(user);
            }

            // Validate that Role is not null or empty
            if (string.IsNullOrEmpty(user.Role))
            {
                ModelState.AddModelError("Role", "Role is required.");
                return View(user);
            }

            // Process the image upload if it exists
            if (user.Image != null && user.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await user.Image.CopyToAsync(memoryStream);
                    user.ImageData = memoryStream.ToArray(); // Store image as byte array
                }
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Password,Email,Gender,Age,Height,Weight,Image")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            try
            {
                if (user.Image != null && user.Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await user.Image.CopyToAsync(memoryStream);
                        user.ImageData = memoryStream.ToArray(); // Store image as byte array
                    }
                }

                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        // GET: Users/Delete/14
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user); // Ensure this returns a single User
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Details/5
        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user); // This should pass a single User model
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
