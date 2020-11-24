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

        public string ProductDescription { get; set; }

        [Required]
        public string ShopeName { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string UserName { get; set; }

        public byte[] Image { get; set; }

    }
}
