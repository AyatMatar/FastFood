using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Models
{
    public class SizeModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int SizeID { get; set; }
        [Required(ErrorMessage = "please fill name")]
        public string Name { get; set; }
    }
}