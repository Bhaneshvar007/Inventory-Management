using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface ICustomer
    {
        Task<List<CustomerModel>> GetAllCustomerAsync();
        Task<string> InsertCustomerAsync(CustomerModel customer);
        Task<string> DeleteCustomerAsync(int id);

        Task<CustomerModel> GetCustomerByIdAsync(int id);
        Task<string> UpdateCustomerAsync(CustomerModel newCustomer);
    }
}
