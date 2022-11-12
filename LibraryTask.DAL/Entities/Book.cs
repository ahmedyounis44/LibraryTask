using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask.DAL.Entities
{
    public class Book: BasicData
    {
        public string Name { get; set; }
        public string BookCover { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal cost { get; set; }
        public int Author_Id { get; set; }
        [ForeignKey("Author_Id")]
        public Author AuthorData { get; set; }

        public int Category_Id { get; set; }
        [ForeignKey("Category_Id")]
        public Category CategoryData { get; set; }
        public int ? SubCategory_Id { get; set; }
        [ForeignKey("SubCategory_Id")]
        public Category SubCategoryData { get; set; }

        [NotMapped]
        public IFormFile Bookimage { get; set; }
    }
}
