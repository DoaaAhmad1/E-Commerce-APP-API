using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _appDBContext;
        public ProductRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext ?? throw new ArgumentNullException(nameof(appDBContext));
        }
        public void CreateProduct(Guid CategoryId, Product product)
        {
            if(CategoryId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }
            if(product==null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            product.CategoryId = CategoryId;
            product.IsDeleted = false;
            _appDBContext.Products.Add(product);
           
        }

        public void DeleteProduct(Product product)
        {
            _appDBContext.Products.Remove(product); 
        }

        public IEnumerable<Product> GetDiscountedProducts()
        {
            return _appDBContext.Products.Where(p => p.IsDiscountedProduct == true && p.IsDeleted==false).ToList();
        }

        public Product GetProduct(Guid CategoryId, Guid ProductId)
        {
           if(CategoryId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }
           if(ProductId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ProductId));
            }
            return _appDBContext.Products.Where(p => p.CategoryId == CategoryId && p.ProductId == ProductId && p.IsDeleted == false).FirstOrDefault();
        }
        public Product GetProductById(Guid ProductId)
        {
            if (ProductId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(ProductId));
            }
            return _appDBContext.Products.SingleOrDefault(p => p.ProductId == ProductId && p.IsDeleted == false);
        }

        public IEnumerable<Product>GetProductByName(string SearchQuery)
        {
            var SearchResult= _appDBContext.Products as IQueryable<Product>;
           if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
               SearchResult= SearchResult.Where(p => p.Name.Contains(SearchQuery) && p.IsDeleted == false);
            }
            return SearchResult.ToList();
        }

        public IEnumerable<Product> GetProducts(Guid CategoryId)
        {
            if(CategoryId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }
            return _appDBContext.Products.Where(p => p.CategoryId == CategoryId && p.IsDeleted == false).ToList();
        }
        public IEnumerable<Product> GetProductsWithoutCategory()
        {
          
            return _appDBContext.Products.Where(p =>  p.IsDeleted == false).ToList();
        }

        public void UpdateProducts(Product product)
        {
            //// no code in this implementation as we are going to use mapper
        }
        public bool Save()
        {
            return (_appDBContext.SaveChanges() >= 0);
        }
    }
}
