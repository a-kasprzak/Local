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

        [Required(ErrorMessage = "Imie jest wymagane")]
        [MinLength(2, ErrorMessage = "Wpisane imie jest zbyt krótkie")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwa sklepu jest wymagana")]
        public string ShopName { get; set; }

        [Required(ErrorMessage = "Nazwa miasta jest wymagana")]
        public string City { get; set; }

        [Required(ErrorMessage = "Mumer telefonu jest wymagany")]
        [MinLength(9, ErrorMessage = "Podany numer nie posiada 9 cyfr")]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "Adres email jest wymagany ")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
