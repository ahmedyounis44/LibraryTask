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
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public  Task<Book> GetBookByID(long ID)
        {

            return _context.Books.Where(x => x.Id == ID).FirstOrDefaultAsync();
        }
      
        public  Task<List<Book>> GetBookList()
        {
            return _context.Books.Include(x=>x.AuthorData).Include(x=>x.CategoryData).Include(x=>x.SubCategoryData).ToListAsync();
                
        }

        public void DeleteBook(long id)
        {
            var existingParent = _context.Books.Where(x => x.Id == id).FirstOrDefault();
            _context.Books.Remove(existingParent);

        }

        public void AddBook(Book obj)
        {
            _context.Books.Add(obj);
        }

        public void UpdateBook(Book obj)
        {
            var existingParent = _context.Books.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (existingParent != null)
            {
                _context.Entry(existingParent).CurrentValues.SetValues(obj);

            }
        }
    }
}
