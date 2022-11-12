using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTask.DAL.Entities
{
    public class Category:BasicData
    {
        public string Name { get; set; }
        public  int?  parent_Id { get; set; }

        [ForeignKey("parent_Id")]
        public ICollection<Category> SubCategories { get; set; }
           = new List<Category>();
    }
}
