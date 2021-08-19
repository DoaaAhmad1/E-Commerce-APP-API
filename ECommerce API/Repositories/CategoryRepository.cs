using ECommerce_API.Interfaces;
using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly AppDBContext _appDBContext;
        public CategoryRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext ?? throw new ArgumentNullException(nameof(appDBContext));
        }
        public void CreateCategory(Category category)
        {
            if(category==null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            category.IsDeleted = false;
            _appDBContext.Categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            if(category==null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _appDBContext.Categories.Remove(category);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _appDBContext.Categories.Where(c=>c.IsDeleted==false).ToList<Category>();
        }

        public void UpdateCategory(Category category)
        {
            //// no code in this implementation as we are going to use mapper
        }
        public bool Save()
        {
            return (_appDBContext.SaveChanges() >= 0);
        }

        public Category GetCategory(Guid CategoryId)
        {
            if(CategoryId==Guid.Empty)
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }
            return _appDBContext.Categories.SingleOrDefault(c => c.CategoryId == CategoryId&&c.IsDeleted==false);
        }
    }
}
