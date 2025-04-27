using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class FitnessDataViewModel
    {
        public User CurrentUser { get; set; }

        public IEnumerable<Workout> Workouts { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Diet> Diets { get; set; }
        public List<Goal> Goals { get; set; } = new List<Goal>();
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<Achievement> Achievements { get; set; } = new List<Achievement>();
        public List<Workout> RecentWorkouts { get; set; } = new List<Workout>();
        public List<Statistic> Statistics { get; set; } = new List<Statistic>();
    }
}
