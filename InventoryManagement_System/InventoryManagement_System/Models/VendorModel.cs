using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement_System.Models
{
    public class VendorModel
    {
        [Key]
        public int VendorId { get; set; }

        [Required(ErrorMessage = "Vendor name is required")]
        [MinLength(3, ErrorMessage = "Vendor name must be at least 3 characters long")]
        [MaxLength(255, ErrorMessage = "Vendor name cannot exceed 255 characters")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = "Vendor email is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format")]
        [MaxLength(255, ErrorMessage = "Vendor email cannot exceed 255 characters")]
        public string VendorEmail { get; set; }

        [Required(ErrorMessage = "Vendor address is required")]
        [MinLength(3, ErrorMessage = "Vendor address must be at least 3 characters long")]
        [MaxLength(255, ErrorMessage = "Vendor address cannot exceed 255 characters")]
        public string VendorAddress { get; set; }

        [Required(ErrorMessage = "Date of sale is required")]
        public DateTime Date_of_sale { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Billing amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Billing amount must be greater than 0")]
        public decimal Billing_amount { get; set; }
       

        [ForeignKey("ProductId")]
        public ProductModel ProductModel { get; set; }

       

        [ForeignKey("CategoryId")]
        public CetegoryModel CetegoryModel { get; set; }

        public static implicit operator VendorModel(List<VendorModel> v)
        {
            throw new NotImplementedException();
        }
    }
}
