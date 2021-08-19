using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Interfaces
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetails(Guid OrderId);
        public OrderDetail GetOrderDetail(Guid OrderId, Guid OrderDetailId);
        void UpdateOrderDetail(OrderDetail OrderDetail);
        void DeleteOrderDetail(OrderDetail OrderDetail);
        public bool Save();
    }
}
