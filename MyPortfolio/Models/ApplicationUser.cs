using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolio.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string  FullName { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime RegisterTime { get; set; } = DateTime.Now;
    }
}
