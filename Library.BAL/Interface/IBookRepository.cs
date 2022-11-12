using LibraryTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask.BAL.Interface
{
    public interface IBookRepository
    {
        Task<Book> GetBookByID(long ID);
        void UpdateBook(Book obj);
        void AddBook(Book obj);
        Task<List<Book>> GetBookList();
        void DeleteBook(long id);
        bool Save();
    }
}
