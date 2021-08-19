using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetUserOrders(string UserId);
        Order GetOrder(Guid OrderId);
        void CreateOrder(Order order);
        void UpdateOrder(Order product);
        void DeleteOrder(Order order);
        public bool Save();


    }
}
