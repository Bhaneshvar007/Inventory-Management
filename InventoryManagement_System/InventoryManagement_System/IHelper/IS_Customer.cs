using InventoryManagement_System.Models;

namespace InventoryManagement_System.IHelper
{
    public interface IS_Customer
    {
        Task<List<CustomerModel>> GetAllCustomers();
        Task<CustomerModel> GetCustomerByIdAsync(int id);
        Task<string> InsertCustomerAsync(VendorCetegoryModel customer);
        Task<string> UpdateCustomerAsync(VendorCetegoryModel customer);
        Task<string> DeleteCustomerAsync(int id);
        Task<List<ProductModel>> GetProductsByCategoryAsync(int categoryId);
        Task<VendorCetegoryModel> GetInsertCustomerDataAsync();
        Task<VendorCetegoryModel> GetUpdateCustomerDataAsync(int id);
    }
}
