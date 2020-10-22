using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThiLai.Models.ModelViews
{
    public class CategoryOfCake
    {
        public int CategoryId { get; set; }
        public List<CakeViewModel> cakes { get; set; }
    }
}
