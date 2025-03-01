using System.ComponentModel.DataAnnotations;
using InventoryManagement_System.Interface;

namespace InventoryManagement_System.Models
{
    public class ProductModel
    {

        [Key]
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product Name must be between 3 and 50 characters.")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Product Quantity is required.")]
        [Range(1, 10000, ErrorMessage = "Product Quantity must be between 1 and 10,000.")]
        public int? ProductQuantity { get; set; }

        [Required(ErrorMessage = "Product Price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Product Price must be between 0.01 and 999,999.99.")]
        public decimal? ProductPrice { get; set; }

        [Required(ErrorMessage = "Product Brand is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product Brand must be between 3 and 50 characters.")]
        public string? ProductBrand { get; set; }

        //[Required(ErrorMessage = "Category is required.")]
        //public int cetegoryId { get; set; }
        public CetegoryModel Cetegory { get; set; }


    }
}
