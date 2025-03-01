using InventoryManagement_System.Models;

namespace InventoryManagement_System.Interface
{
    public interface IProduct
    {
       Task<List<ProductModel>> GetProductAsync();
        //Task<string> CreateProductAsynce(ViewProductModel productModel);
        Task<string> CreateProductAsynce(ProductModel productModel);
        Task<ProductModel> GetProductByIdAsync(int id);

        Task<string> DeleteProductAsync(int productId);
        Task<string> UpdateProductAsync(ProductModel product);

        Task<List<ProductModel>> GetProductsByCategoryAsync(int id);

    }
}
