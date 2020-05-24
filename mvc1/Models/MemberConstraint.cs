using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc1.Models
{
    public class MemberConstraint
    {
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[a-zA-Z]{4,}$", ErrorMessage = "4 minimum english letter required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Username is requried")]
        [RegularExpression(@"^\w{6,}$", ErrorMessage = "6 minimum english letters and digits including underline")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is required")]
        [RegularExpression(@"^\w{6,}$", ErrorMessage = "6 minimum english letters and digits including underline")]
        public string password { get; set; }
    }

    [MetadataType(typeof(MemberConstraint))]
    public partial class member
    {

    }
}