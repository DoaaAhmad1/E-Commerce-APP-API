using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/shoppingCart")]
    [Authorize(Roles = "admin, user")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<ShoppingCartItem>> GetShoppingCartItems()
        {
            var ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = ShoppingCartItems;
            return Ok(ShoppingCartItems);
        }
        [HttpPost("{productId}")]
        public ActionResult AddToShoppingCart(Guid productId)
        {
            var SelectedProduct = _productRepository.GetProductById(productId);
            if (SelectedProduct == null)
            {
                return NotFound();
            }
            _shoppingCart.AddToCart(SelectedProduct, 1);
            return NoContent();
        }
        [HttpDelete("{productId}")]
        public ActionResult RemoveFromShoppingCart(Guid productId)
        {
            var SelectedProduct = _productRepository.GetProductById(productId);
            if (SelectedProduct == null)
            {
                return NotFound();
            }
            _shoppingCart.RemoveFromCart(SelectedProduct);
            return NoContent();
        }

    }
}
