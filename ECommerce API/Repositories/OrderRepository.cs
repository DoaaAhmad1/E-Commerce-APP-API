using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _appDBContext;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(AppDBContext appDBContext, ShoppingCart shoppingCart)
        {
            _appDBContext = appDBContext;
            _shoppingCart = shoppingCart;

        }
        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            order.OrderPlace = DateTime.Now;
            order.IsDeleted = false;
            order.OrderStatus = "pending";
            _appDBContext.Orders.Add(order);
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();//.ShoppingCartItems;
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    ProductId = item.product.ProductId,
                    OrderId = order.OrderId,
                    Price = item.product.Price,
                    ProductName = item.product.Name
                };
                _appDBContext.OrderDetails.Add(orderDetail);
            }
        }

        public void DeleteOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _appDBContext.Orders.Remove(order);
        }

        public Order GetOrder(Guid OrderId)
        {
            if (OrderId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(OrderId));
            }
            return _appDBContext.Orders.SingleOrDefault(o => o.OrderId == OrderId && o.IsDeleted == false);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _appDBContext.Orders.Where(o => o.IsDeleted == false).ToList<Order>();
        }
        //GetOrdersByUser
        public IEnumerable<Order> GetUserOrders(string UserId)
        {
            
            return _appDBContext.Orders.Where(o => o.IsDeleted == false&&o.user.Id== UserId).ToList<Order>();
        }

        public void UpdateOrder(Order product)
        {
            //// no code in this implementation as we are going to use mapper
        }
        public bool Save()
        {
            return (_appDBContext.SaveChanges() >= 0);
        }
    }
}
