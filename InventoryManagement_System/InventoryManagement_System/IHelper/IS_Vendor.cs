using InventoryManagement_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement_System.IHelper
{
    public interface IS_Vendor
    {
        Task<List<VendorModel>> GetVendorListAsync();
        Task<VendorCetegoryModel> InsertVendor();
        Task<string> InsertVendorAsync(VendorCetegoryModel vendor);
        Task<VendorCetegoryModel> GetVendorByIdAsync(int id1 , int id2);
        Task<string> UpdateVendorAsync(VendorCetegoryModel vendor);
        Task<string> DeleteVendorAsync(int id1 , int id2);
        Task<List<ProductModel>> GetProductsByCategoryAsync(int categoryId);
    }
}
