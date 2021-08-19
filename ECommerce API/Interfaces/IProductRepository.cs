using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts(Guid CategoryId);
        public Product GetProduct(Guid CategoryId, Guid ProductId);

        //To Be Used only With ShoppingCartController just for simplicity
        public Product GetProductById(Guid ProductId);
        IEnumerable<Product> GetDiscountedProducts();
        IEnumerable<Product> GetProductsWithoutCategory();
        IEnumerable<Product> GetProductByName(string ProductName); // Search by Name
        void CreateProduct(Guid CategoryId ,Product product);
        void UpdateProducts(Product product);
        void DeleteProduct(Product product);

        public bool Save();
    }

}
