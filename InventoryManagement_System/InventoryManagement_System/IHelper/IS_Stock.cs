using InventoryManagement_System.Models;

namespace InventoryManagement_System.IHelper
{
    public interface IS_Stock
    {
        Task<VendorCetegoryModel> GetVendorCategoryData();
        Task<IEnumerable<CetegoryModel>> GetCategoriesByVendor(int vendorId);
        Task<string> InsertOrUpdateStock(VendorCetegoryModel vendor);
    }
}
