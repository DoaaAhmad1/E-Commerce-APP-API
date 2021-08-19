using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDBContext _appDBContext;
        public OrderDetailRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public void DeleteOrderDetail(OrderDetail OrderDetail)
        {
            _appDBContext.OrderDetails.Remove(OrderDetail);
        }

        public OrderDetail GetOrderDetail(Guid OrderId, Guid OrderDetailId)
        {
            if(OrderId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(OrderId));
            }
            if(OrderDetailId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(OrderDetailId));
            }
            return _appDBContext.OrderDetails.Where(o => o.OrderId == OrderId && o.OrderDetailId == OrderDetailId).SingleOrDefault();
        }
        
        public void UpdateOrderDetail(OrderDetail OrderDetail)
        {
            //// no code in this implementation as we are going to use mapper
        }
        public bool Save()
        {
            return (_appDBContext.SaveChanges() >= 0);
        }

        public IEnumerable<OrderDetail> GetOrderDetails(Guid OrderId)
        {
            if(OrderId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(OrderId));
            }
            return _appDBContext.OrderDetails.Where(o => o.OrderId == OrderId).ToList();
        }
    }
}
