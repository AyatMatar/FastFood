using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Models
{
    public class ItemModel
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int ItemID { get; set; }

        [Required (ErrorMessage ="please fill name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name ="imgage")]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "please fill catigory")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        
        public List<Category> Categories { get; set; }
    }
}