using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Dtos
{
    public class OrderDetailForUpdateAndCreateDto
    {
 
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal Price { get; set; }
        

    }
}
