using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Models
{
    public class CartItem
    {
        public int Quantity { get; set; }
        public int ItemSizePrice { get; set; }
        public int UserID { get; set; }
        

    }
}