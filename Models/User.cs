using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Models
{
    public class User
    {
        public string Role { get; set; } // e.g., "Admin" or "User"
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public byte[] ImageData { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Password { get; set; } // Ensure this is stored securely in a production app
    }
}
