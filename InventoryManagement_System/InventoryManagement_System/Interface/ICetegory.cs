using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface ICetegory
    {
        Task<List<CetegoryModel>> GetCategoryAsync();
        Task<CetegoryModel> GetCategoryByIdAsync(int id);
        Task<string> InsertCetegoryAsync(string cetegoryName);
        Task<string> UpdateCetegoryAsync(CetegoryModel cetegory);

        Task<List<CetegoryModel>> GetCetegoryByVendorAsync(int id);

        Task<string> DeleteCetegoryAsync(int id);
         
    }
}
