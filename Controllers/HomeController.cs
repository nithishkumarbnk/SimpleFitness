using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FitnessDbContext _context;

        public HomeController(ILogger<HomeController> logger, FitnessDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get the logged-in user's email from claims
            var email = User.FindFirstValue(ClaimTypes.Email);

            // Retrieve the user based on their email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Populate the view model with the user's details
            var viewModel = new FitnessDataViewModel
            {
                CurrentUser = user
            };
            var model = new FitnessDataViewModel
            {
                Goals = await _context.Goals.ToListAsync(),
                RecentWorkouts = await _context.Workouts
                .OrderByDescending(w => w.Date)
                .Take(3) // Last 3 workouts
                .ToListAsync(),
                Achievements = await _context.Achievements.ToListAsync(),
                Statistics = await _context.Statistics.ToListAsync(),
                Schedules = await _context.Schedules
                .OrderBy(s => s.Date)
                .Take(3) // Next 3 schedule items
                .ToListAsync()
            };

            // Pass the view model to the view
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
      

        public async Task<IActionResult> Schedules()
        {
            var schedules = await _context.Schedules.ToListAsync();
            var model = new FitnessDataViewModel { Schedules = schedules };
            return View(model);
        }

        public async Task<IActionResult> Achievements()
        {
            var achievements = await _context.Achievements.ToListAsync();
            var model = new FitnessDataViewModel { Achievements = achievements };
            return View(model);
        }

        public async Task<IActionResult> Statistics()
        {
            var statistics = await _context.Statistics.ToListAsync();
            var model = new FitnessDataViewModel { Statistics = statistics };
            return View(model);
        }

        public IActionResult Settings()
        {
            // Assuming settings don't require loading from the database
            return View();
        }
        public async Task<IActionResult> Dashboard()
        {
            // Fetch data from the database for each section
            var goals = await _context.Goals.ToListAsync();
            var workouts = await _context.Workouts
                .OrderByDescending(w => w.Date)
                .Take(3) // Last 3 workouts
                .ToListAsync();
            var achievements = await _context.Achievements.ToListAsync();
            var statistics = await _context.Statistics.ToListAsync();
            var schedule = await _context.Schedules
                .OrderBy(s => s.Date)
                .Take(3) // Next 3 schedule items
                .ToListAsync();

            // Pass data to the view using ViewBag
            ViewBag.Goals = goals;
            ViewBag.RecentWorkouts = workouts;
            ViewBag.Achievements = achievements;
            ViewBag.Statistics = statistics;
            ViewBag.Schedule = schedule;

            return View();
        }
    }
}
