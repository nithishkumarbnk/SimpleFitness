using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } // Example additional property
    public int Age { get; set; } // Example additional property
    // Add any other properties you need
}
