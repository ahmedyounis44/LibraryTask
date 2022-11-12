using LibraryTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask.BAL.Interface
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorByID(long ID);
        void UpdateAuthor(Author obj);
        void AddAuthor(Author obj);
        Task<List<Author>> GetAuthorList();
        void DeleteAuthor(long id);
        bool Save();
    }
}
