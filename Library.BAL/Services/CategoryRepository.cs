using LibraryTask.BAL.Interface;
using LibraryTask.DAL;
using LibraryTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask.BAL.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext _context;

        public CategoryRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public  Task<Category> GetCategoryByID(long ID)
        {

            return _context.Categories.Where(x => x.Id == ID).FirstOrDefaultAsync();
        }
      
        public  Task<List<Category>> GetCategoryList()
        {
            return _context.Categories.FromSqlRaw("GetCategories").ToListAsync();
                
        }

        public Task<List<Category>> GetSubCategoryList(int CatId)
        {
            return _context.Categories.FromSqlRaw($"GetSubCategories {CatId}").ToListAsync();

        }

        public void DeleteCategory(long id)
        {
            var existingParent = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            _context.Categories.Remove(existingParent);

        }

        public void AddCategory(Category obj)
        {
            _context.Categories.Add(obj);
        }

        public void UpdateCategory(Category obj)
        {
            var existingParent = _context.Categories.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (existingParent != null)
            {
                _context.Entry(existingParent).CurrentValues.SetValues(obj);

            }
        }
    }
}
