using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderDetailId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Amount { get; set; }
        public decimal? Price { get; set; }
        public string ProductName { get; set; }
        public /*virtual*/ Product Product { get; set; }
        public /*virtual*/ Order Order { get; set; }
    }
}
