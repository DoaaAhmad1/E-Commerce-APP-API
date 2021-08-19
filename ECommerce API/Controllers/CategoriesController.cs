using AutoMapper;
using ECommerce_API.Dtos;
using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using ECommerce_API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet()]
        //[Authorize(Roles = "admin, user")]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            var CategoriesFromRepo = _categoryRepository.GetCategories();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(CategoriesFromRepo));
        }
        [HttpGet("{categoryId}",Name= "GetCategory")]
        [Authorize(Roles = "admin, user")]
        public IActionResult GetCategory(Guid categoryId)
        {
            var CategoryFromRepo = _categoryRepository.GetCategory(categoryId);
            if(CategoryFromRepo==null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryDto>(CategoryFromRepo));
        }
        [HttpPost()]
        [Authorize(Roles = "admin")]
        public ActionResult<CategoryDto> CreateCategory(CategoryForUpdateAndCreateDto category)
        {
           
            var CategoryEntity = _mapper.Map<Category>(category);
            _categoryRepository.CreateCategory(CategoryEntity);
            _categoryRepository.Save();

            var CategoryToReturn = _mapper.Map<CategoryDto>(CategoryEntity);
            return CreatedAtRoute("GetCategory", new { categoryId = CategoryToReturn.CategoryId }, CategoryToReturn);
        }
        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCategory (Guid categoryId)
        {
            var CategoryFromRepo = _categoryRepository.GetCategory(categoryId);
            if(CategoryFromRepo==null)
            {
                return NotFound();
            }
            //  _categoryRepository.DeleteCategory(CategoryFromRepo);
            //SoftDelete
            CategoryFromRepo.IsDeleted = true;
            _categoryRepository.Save();
            return NoContent();
        }
        [HttpPut("{categoryId}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateCategory(Guid categoryId, CategoryForUpdateAndCreateDto category)
        {
            var CategoryFromRepo = _categoryRepository.GetCategory(categoryId);
            if (CategoryFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(category, CategoryFromRepo);
            // AutoMapper Here Is Used For update  , This Function Has No Implementation
            //  _categoryRepository.UpdateCategory(Category);
            _categoryRepository.Save();
            return NoContent();
        }



    }
}
