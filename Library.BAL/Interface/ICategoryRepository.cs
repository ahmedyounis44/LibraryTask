using LibraryTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask.BAL.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByID(long ID);
        void UpdateCategory(Category obj);
        void AddCategory(Category obj);
        Task<List<Category>> GetCategoryList();
        Task<List<Category>> GetSubCategoryList(int CatId);
        void DeleteCategory(long id);
        bool Save();
    }
}
