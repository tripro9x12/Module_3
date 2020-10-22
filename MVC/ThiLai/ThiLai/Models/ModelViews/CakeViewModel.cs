using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThiLai.Models.ModelViews
{
    public class CakeViewModel
    {
        public int CakeId { get; set; }
        public string TenBanh { get; set; }
        public string ThanhPhan { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HSD { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime NSX { get; set; }
        public int GiaBan { get; set; }
        public bool DaXoa { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
