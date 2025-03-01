using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface IVendor
    {
        Task<List<VendorModel>> GetVendorListAsync();
        Task<string> InsertVendorAsync(VendorModel vendor);
        Task<VendorModel> GetVendorByIdAsync(int id);
        Task<string> UpdateVendorAsync(VendorModel vendor);
        Task<string> DeleteVendorAsync(int VendorId);
    }
}
