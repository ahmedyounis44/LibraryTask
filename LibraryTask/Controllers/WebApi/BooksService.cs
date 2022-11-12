using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryTask.BAL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MOD.Controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BooksService : Controller
    {
        private readonly IBookRepository _IBookRepository;
        public BooksService( IBookRepository BookRepository)
        {

            _IBookRepository = BookRepository;
        }
        [HttpGet]
        [Route("BooksList")]
        public IActionResult BooksList()
        {

            try
            {
                var result = _IBookRepository.GetBookList();
                return Ok(new { status = true, message = result });
            }
            catch (Exception ex)
            {

                return Ok(new { status = false, message = "" });
            }
            
        }

       
    }
}
