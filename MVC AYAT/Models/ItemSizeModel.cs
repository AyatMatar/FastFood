using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Models
{
    public class ItemSizeModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ItemSizePricID { get; set; }
        [Required(ErrorMessage = "please fill name")]
        [Display(Name = "Item")]
        public int ItemID { get; set; }
        [Required(ErrorMessage = "please fill name")]
        [Display(Name = "Size")]
        public int SizeID { get; set; }
        [Required(ErrorMessage = "please fill name")]
        [Range(.20,50,ErrorMessage = "please pric>0")]
        public double pric { get; set; }

        public virtual List<Item> Items { get; set; }
        public virtual List<Size> Sizes{ get; set; }
    }
}