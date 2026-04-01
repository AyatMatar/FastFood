using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_AYAT.Models
{
    public class UserModel
    {
        
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }
        [Required(ErrorMessage = "please fill name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "please fill Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "please fill Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "please fill mobilenumber")]
        [StringLength(10,ErrorMessage = "Fols mobilenumber")]
        public string Mobilenumber { get; set; }
        [ScaffoldColumn(false)]
        public string UserType { get; set; }
        [ScaffoldColumn(false)]
        public string Status { get; set; }
    }
}