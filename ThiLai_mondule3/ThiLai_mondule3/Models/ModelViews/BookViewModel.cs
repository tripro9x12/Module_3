using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThiLai_mondule3.Models.ModelViews
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
        public int Amount { get; set; }
        public string CategoryName { get; set; }
    }
}
