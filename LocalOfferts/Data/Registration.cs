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
        [Required(ErrorMessage = "Imie musi zostać wprowadzone")]
        [MinLength(2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nazwa skelpu musi zostać wprowadzona")]
        public string ShopName { get; set; }
        [Required(ErrorMessage = "Nazwa miasta musi zostać wprowadzona")]
        public string City { get; set; }
        [Required]
        [MinLength(9)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
