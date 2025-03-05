using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface IVendor
    {
        Task<List<VendorModel>> GetVendorListAsync();
        Task<List<VendorModel>> GetVendorProductListAsync();
        Task<string> InsertVendorAsync(VendorModel vendor);
        Task<VendorModel> GetVendorByIdAsync(int id1 , int id2);
        Task<string> UpdateVendorAsync(VendorModel vendor);
        Task<string> DeleteVendorAsync(int VendorId , int productId);
       
    }
}
