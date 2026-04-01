using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Models
{
    public class CategoryModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "please fill name")]
        public string Name { get; set; }
    }
}