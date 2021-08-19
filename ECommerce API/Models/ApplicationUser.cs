using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Gender { get; set; } // Enum
        public DateTime? BrithDate { get; set; }
        public string Photo { get; set; }
        public  IEnumerable<Order> Orders { get; set; }
    }
}
