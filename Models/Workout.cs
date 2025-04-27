using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Duration { get; set; } // in minutes
        public DateTime Date { get; set; }
        public int CaloriesBurned { get; set; }
    }

}