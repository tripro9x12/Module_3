using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.ViewsModel
{
    public class HomeEditViewModel : HomeCreateViewModel
    {
        public int Id { get; set; }
        public string AvatarPath { get; set; }
    }
}
