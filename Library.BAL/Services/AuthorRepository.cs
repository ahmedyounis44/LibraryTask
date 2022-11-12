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
    public class AuthorRepository: IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public  Task<Author> GetAuthorByID(long ID)
        {

            return _context.Authors.Where(x => x.Id == ID).FirstOrDefaultAsync();
        }
      
        public  Task<List<Author>>  GetAuthorList()
        {
            return _context.Authors.FromSqlRaw("GetAuthors").ToListAsync();
                
        }

        public void DeleteAuthor(long id)
        {
            var existingParent = _context.Authors.Where(x => x.Id == id).FirstOrDefault();
            _context.Authors.Remove(existingParent);

        }

        public void AddAuthor(Author obj)
        {
            _context.Authors.Add(obj);
        }

        public void UpdateAuthor(Author obj)
        {
            var existingParent = _context.Authors.Where(x => x.Id == obj.Id).FirstOrDefault();
            if (existingParent != null)
            {
                _context.Entry(existingParent).CurrentValues.SetValues(obj);

            }
        }
    }
}
