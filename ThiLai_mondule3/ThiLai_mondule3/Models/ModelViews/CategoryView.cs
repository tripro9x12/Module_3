using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiLai_mondule3.Models.ModelViews
{
    public class CategoryView
    {
        public int CategoryId { get; set; }
        public List<BookViewModelDetail> books{get;set;}
        
    }
}
