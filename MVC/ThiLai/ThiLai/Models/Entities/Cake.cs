using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThiLai.Models.Entities
{
    public class Cake
    {
        public int CakeId { get; set; }
        [Required]
        public string TenBanh { get; set; }
        [Required]
        public string ThanhPhan { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime HSD { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime NSX { get; set; }
        [Required]
        public int GiaBan { get; set; }
        [DefaultValue("false")]
        public bool DaXoa { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
