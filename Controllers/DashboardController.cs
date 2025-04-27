using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly FitnessDbContext _context;

        public DashboardController(FitnessDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> goaldetails(int id)
        {
            // Fetch the goal from the database based on the ID
            var goal = await _context.Goals.FirstOrDefaultAsync(g => g.Id == id);

            // Check if the goal exists
            if (goal == null)
            {
                return NotFound(); // Return 404 if the goal does not exist
            }

            // Pass the goal to the view
            return View(goal);
        }
        public async Task<IActionResult> workouts(int id)
        {
            // Fetch the workout from the database based on the ID
            var workout = await _context.Workouts.FirstOrDefaultAsync(w => w.Id == id);

            // Check if the workout exists
            if (workout == null)
            {
                return NotFound(); // Return 404 if the workout does not exist
            }

            // Pass the workout to the view
            return View(workout);
        }
        public async Task<IActionResult> statsticdetails(int id)
        {
            // Fetch the statistic based on the ID (assuming you have a model for statistics)
            var statistic = await _context.Statistics.FirstOrDefaultAsync(s => s.Id == id);

            // Check if the statistic exists
            if (statistic == null)
            {
                return NotFound(); // Return 404 if the statistic does not exist
            }

            // Pass the statistic to the view
            return View(statistic);
        }
        public async Task<IActionResult> schedule(int id)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule); // Pass the schedule to the view
        }

        public async Task<IActionResult> achive(int id)
        {
            var achievement = await _context.Achievements.FirstOrDefaultAsync(a => a.Id == id);
            if (achievement == null)
            {
                return NotFound();
            }
            return View(achievement);
        }


        public async Task<IActionResult> AllGoals()
        {
            var goals = await _context.Goals.ToListAsync();
            return View(goals); // Make sure there's a corresponding AllGoals.cshtml view
        }
        public async Task<IActionResult> AllWorkouts()
        {
            var workouts = await _context.Workouts.ToListAsync();
            return View(workouts); // Make sure there's a corresponding AllWorkouts.cshtml view
        }
        public async Task<IActionResult> AllAchive()
        {
            // Query the Achievements instead of Workouts
            var achive = await _context.Achievements.ToListAsync();
            return View(achive); // Pass the correct model to the view
        }

        public async Task<IActionResult> AllSchedule()
        {
            var schedules = await _context.Schedules.ToListAsync(); // Fetch schedules instead of workouts
            return View(schedules); // Pass the list of schedules to the view
        }

        public async Task<IActionResult> AllStatistics()
        {
            var statistics = await _context.Statistics.ToListAsync(); // Fetch the statistics from the database
            return View(statistics); // Pass the statistics list to the view
        }




    }
}
