using System;
using System.ComponentModel.DataAnnotations;

namespace CIS4930_Mini_Project_1.Models
{
    public class ComparePass
    {
            [Required(ErrorMessage = "Please Insert Pass")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Confirm Pass is needed")]
        [DataType(DataType.Password)]
        [Compare("Pass")]
        public string confirmPass { get; set; }
        
    }
}
