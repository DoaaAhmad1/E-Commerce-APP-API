using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public List<OrderDetail> OrderLines { get; set; }= new List<OrderDetail>();
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public decimal OrderTotal { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? OrderPlace { get; set; }
        public ApplicationUser user { get; set; }
        [Required]
        public string OrderStatus { get; set; }
    }
}
