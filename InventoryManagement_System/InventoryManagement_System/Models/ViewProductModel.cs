namespace InventoryManagement_System.Models
{
    public class ViewProductModel
    {   
        public IEnumerable<CetegoryModel> cetegoryModels { get; set; }

        public ProductModel Product { get; set; }
        public int cetegoryId { get; set; }
       
    }
}
