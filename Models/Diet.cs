using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Diet
    {
        public int Id { get; set; }
        public string FoodItem { get; set; }
        public int Calories { get; set; }
        public DateTime Date { get; set; }
    }

}