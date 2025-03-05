using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface IStock
    {
        Task<string> InsertOrUpdateStockAsync(VendorModel viewModel);
    }
}
