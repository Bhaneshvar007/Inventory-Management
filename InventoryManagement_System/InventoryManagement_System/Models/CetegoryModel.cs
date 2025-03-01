using System.ComponentModel.DataAnnotations;

namespace InventoryManagement_System.Models
{
    public class CetegoryModel
    {
        public int cetegoryId { get; set; }


        [Required(ErrorMessage = "Cetegory Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Cetegory Name must be between 3 and 50 characters.")]
        public string cetegoryName { get; set; }

    }
}
