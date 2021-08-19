using AutoMapper;
using ECommerce_API.Dtos;
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
   
    public class ProductsController: ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        
        public ProductsController(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("api/ProductsWithPromotions")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ProductDto>> GetDiscountedProducts()
        {
            var DiscountedProductsFromRepo = _productRepository.GetDiscountedProducts();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(DiscountedProductsFromRepo));
        }
        [HttpGet("api/ProductsWithoutCategory")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ProductDto>> GetProductsWithoutCategory()
        {
            var ProductsWithoutCategoryFromRepo = _productRepository.GetProductsWithoutCategory();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(ProductsWithoutCategoryFromRepo));
        }
        [HttpGet("api/searchforproducts/{searchQuery}")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<ProductDto>> GetProductByName(string searchQuery)
        {
            var ProductsFromRepo = _productRepository.GetProductByName(searchQuery);
            if(ProductsFromRepo==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(ProductsFromRepo));
        }
       
        [HttpGet("api/categories/{categoryId}/products/{productId}",Name = "GetProductForCategory")]
        [Authorize(Roles = "admin")]
        public ActionResult<ProductDto> GetProductForCategory(Guid categoryId,Guid productId)
        {
            var Category = _categoryRepository.GetCategory(categoryId);
            if(Category==null)
            {
                return NotFound();
            }
            var ProductForCategoryFromRepo = _productRepository.GetProduct(categoryId, productId);
            if(ProductForCategoryFromRepo==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductDto>(ProductForCategoryFromRepo));
        }
       
        [HttpGet("api/categories/{categoryId}/products")]
        [Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<ProductDto>> GetProductForCategory(Guid categoryId)
        {
            var category = _categoryRepository.GetCategory(categoryId); 
            if(category==null)
            {
                return NotFound();
            }
            var ProductsForCategoryFromRepo = _productRepository.GetProducts(categoryId);
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(ProductsForCategoryFromRepo));
        }
       
        [HttpPost("api/categories/{categoryId}/products")]
        [Authorize(Roles = "admin")]
        public ActionResult<ProductDto>CreateProductForCategory(Guid categoryId, ProductForUpdateAndCreateDto product)
        {
            var category = _categoryRepository.GetCategory(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            var ProductEntity = _mapper.Map<Product>(product);
            _productRepository.CreateProduct(categoryId, ProductEntity);
            _productRepository.Save();

            var ProductToReturn = _mapper.Map<ProductDto>(ProductEntity);
            return CreatedAtRoute("GetProductForCategory", new { categoryId = categoryId, ProductToReturn.ProductId }, ProductToReturn);
        }
       
        [HttpPut("api/categories/{categoryId}/products/{productId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateProductForCategory(Guid productId,Guid categoryId,ProductForUpdateAndCreateDto product)
        {
            var Category = _categoryRepository.GetCategory(categoryId);
            if(Category==null)
            {
                return NotFound();
            }
            var ProductForCategoryFromRepo = _productRepository.GetProduct(categoryId, productId);
            if(ProductForCategoryFromRepo==null)
            {
                return NotFound();
            }
            _mapper.Map(product, ProductForCategoryFromRepo);
            // AutoMapper Here Is Used For update  , This Function Has No Implementation
            //_productRepository.UpdateProducts(product);
            _productRepository.Save();
            return NoContent();
        }
        
        [HttpDelete("api/categories/{categoryId}/products/{productId}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteProductForCategory (Guid categoryId, Guid productId)
        {
            var Category = _categoryRepository.GetCategory(categoryId);
            if (Category == null)
            {
                return NotFound();
            }
            var ProductForCategoryFromRepo = _productRepository.GetProduct(categoryId, productId);
            if (ProductForCategoryFromRepo == null)
            {
                return NotFound();
            }
            //SoftDelete
            ProductForCategoryFromRepo.IsDeleted =true;
           // _productRepository.DeleteProduct(ProductForCategoryFromRepo);
            _productRepository.Save();
            return NoContent();
        }

    }
}
