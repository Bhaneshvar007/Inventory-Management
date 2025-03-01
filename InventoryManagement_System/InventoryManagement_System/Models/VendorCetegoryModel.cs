namespace InventoryManagement_System.Models
{
    public class VendorCetegoryModel
    {
        public IEnumerable<CetegoryModel> V_cetegory { get; set; }
        public IEnumerable<ProductModel> V_products { get; set; }
        public VendorModel vendorModel { get; set; }
        public CustomerModel Customers { get; set; }

        public string modelType { get; set; }
        public int vc_cetegoryId { get; set; }
        public int vc_productId { get; set; }
    }
}
