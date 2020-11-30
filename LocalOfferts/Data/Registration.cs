using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LocalOfferts.Data
{
    public class Registration
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Name is too short")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Cannot be empty")]
        public string ShopName { get; set; }

        [Required(ErrorMessage = "Cannot be empty")]
        public string City { get; set; }

        [Required]
        [MinLength(9, ErrorMessage = "Minimum 9 digitals")]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
