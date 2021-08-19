using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Models
{
    public class ShoppingCart
    {
        //AppDBContext is your DbContext Class
        private readonly AppDBContext _appDBContext;
        public ShoppingCart(AppDBContext appDbContext)
        {
            _appDBContext = appDbContext;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            //  .HttpContext.Session;
            var context = services.GetService<AppDBContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product, int amount)
        {
            var shopingCartItem = _appDBContext.ShoppingCartItems.SingleOrDefault(s => s.product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shopingCartItem == null)
            {
                shopingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    product = product,
                    Amount = 1
                };
                _appDBContext.ShoppingCartItems.Add(shopingCartItem);

            }
            else
            {
                shopingCartItem.Amount++;
            }
            _appDBContext.SaveChanges();
        }

        public /*int*/ void RemoveFromCart(Product product)
        {
            var shoppingCartItem = _appDBContext.ShoppingCartItems.SingleOrDefault(s => s.product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            //var LocalAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    // LocalAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDBContext.ShoppingCartItems.Remove(shoppingCartItem);
                }

            }
            _appDBContext.SaveChanges();
            // return LocalAmount;
        }

        public /*int*/ void RemoveAllFromCart(Product product)
        {
            var shoppingCartItem = _appDBContext.ShoppingCartItems.SingleOrDefault(s => s.product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null){
                _appDBContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
            _appDBContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems = _appDBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(p => p.product).ToList());

            //  .Include(s => s.Drink).ToList());
        }

        public void ClearCart()
        {
            var CartItems = _appDBContext.ShoppingCartItems.Where(Cart => Cart.ShoppingCartId == ShoppingCartId);

            _appDBContext.ShoppingCartItems.RemoveRange(CartItems);
            _appDBContext.SaveChanges();
        }

        public decimal GetShopingCartTotal()
        {
            var Total = _appDBContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.product.Price * c.Amount).Sum();
            return Total;
        }
    }
}
