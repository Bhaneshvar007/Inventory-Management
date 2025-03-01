using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement_System.Models
{
    public class CustomerModel
    {
        [Key]
        public int Cust_id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 100 characters.")]
        public string Customer_name { get; set; }

        [Required(ErrorMessage = "Customer email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter a valid email address.")]
        public string Customer_email { get; set; }

        [StringLength(300, MinimumLength = 3, ErrorMessage = "Customer address must be between 3 and 300 characters.")]
        public string Customer_address { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date_of_purchess { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Purchase amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Purchase amount must be greater than 0.")]
        public decimal Purchess_amount { get; set; }

        //[Required]
        public CetegoryModel Cetegory { get; set; }

        //[Required]
        public ProductModel Product { get; set; }

       
    }
}
