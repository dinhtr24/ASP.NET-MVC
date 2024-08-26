using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TranHoangDinh_2120110042.Context;

namespace TranHoangDinh_2120110042.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
    }
}