using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThiLai_mondule3.Models.ModelViews
{
    public class CreateBook
    {
        public string BookName { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
