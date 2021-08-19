using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Dtos
{
    public class UserInfoForUpdateDto
    {
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime? BrithDate { get; set; }
        public string Photo { get; set; }
    }
}
