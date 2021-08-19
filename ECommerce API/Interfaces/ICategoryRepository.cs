using ECommerce_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce_API.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        public Category GetCategory(Guid CategoryId);
        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        public bool Save();
    }
}
