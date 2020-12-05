using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LocalOfferts.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public string City { get; set; }

        [MaxLength(200,ErrorMessage ="Max 200 znaków")]
        public string ProductDescription { get; set; }

        [Required]
        public string ShopeName { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        public string UserName { get; set; }

        public byte[] Image { get; set; }

    }
}
