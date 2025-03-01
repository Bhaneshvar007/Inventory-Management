using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using InventoryManagement_System.Services;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement_System.Helper
{
    public class VendorHelper : IS_Vendor
    {
        private readonly IVendor _vendor;
        private readonly IProduct _product;
        private readonly ICetegory _category;

        public VendorHelper(IVendor vendor, IProduct product, ICetegory category)
        {
            _vendor = vendor;
            _product = product;
            _category = category;
        }

        public async Task<List<VendorModel>> GetVendorListAsync()
        {
            return await _vendor.GetVendorListAsync();
        }

       
        public async Task<VendorCetegoryModel> InsertVendor()
        {
            var viewModel = new VendorCetegoryModel();
            var categories = await _category.GetCategoryAsync();

            if (categories.Count > 0 )
            {
                viewModel = new VendorCetegoryModel
                {
                    modelType = "vendorModel",
                    V_cetegory = categories,
                    vendorModel = new VendorModel()
                };
            }

            return viewModel;
        }

        public async Task<string> InsertVendorAsync(VendorCetegoryModel vendor)
        {
            var newVendor = new VendorModel
            {
                VendorId = vendor.vendorModel.VendorId,
                VendorName = vendor.vendorModel.VendorName,
                VendorAddress = vendor.vendorModel.VendorAddress,
                VendorEmail = vendor.vendorModel.VendorEmail,
                Billing_amount = vendor.vendorModel.Billing_amount,
                Date_of_sale = vendor.vendorModel.Date_of_sale,
                Quantity = vendor.vendorModel.Quantity,
                CetegoryModel = new CetegoryModel { cetegoryId = vendor.vc_cetegoryId },
                ProductModel = new ProductModel { ProductId = vendor.vc_productId }
            };



            // Stock Management 
            var products = await _product.GetProductAsync();
            if (products == null || !products.Any())
            {
                return "No products found.";
            }
            var existingProduct =  products.FirstOrDefault(p => p.ProductId == newVendor.ProductModel?.ProductId);

            if (existingProduct != null)
            {
                int updatedQuantity = (int)(existingProduct.ProductQuantity + newVendor.Quantity);

                var newProduct = new ProductModel
                {
                    ProductId = existingProduct.ProductId,
                    ProductName = existingProduct.ProductName,
                    ProductPrice = existingProduct.ProductPrice,
                    ProductBrand = existingProduct.ProductBrand,
                    ProductQuantity = updatedQuantity,
                    Cetegory = new CetegoryModel()
                    {
                        cetegoryId = newVendor.CetegoryModel.cetegoryId
                    }
                };

                var res = await _product.UpdateProductAsync(newProduct);
            }

            

            return await _vendor.InsertVendorAsync(newVendor);
        }

        public async Task<VendorCetegoryModel> GetVendorByIdAsync(int id)
        {
            var vendor = await _vendor.GetVendorByIdAsync(id);
            if (vendor == null) return null;

            return new VendorCetegoryModel
            {
                modelType = "vendorModel",
                vendorModel = vendor,
                V_cetegory = await _category.GetCategoryAsync(),
                vc_cetegoryId = vendor.CetegoryModel.cetegoryId,
                V_products = await _product.GetProductAsync(),
                vc_productId = vendor.ProductModel?.ProductId ?? 0
            };
        }

        public async Task<string> UpdateVendorAsync(VendorCetegoryModel vendor)
        {
            var updatedVendor = new VendorModel
            {
                VendorId = vendor.vendorModel.VendorId,
                VendorName = vendor.vendorModel.VendorName,
                VendorAddress = vendor.vendorModel.VendorAddress,
                VendorEmail = vendor.vendorModel.VendorEmail,
                Billing_amount = vendor.vendorModel.Billing_amount,
                Date_of_sale = vendor.vendorModel.Date_of_sale,
                Quantity = vendor.vendorModel.Quantity,
                CetegoryModel = new CetegoryModel { cetegoryId = vendor.vc_cetegoryId },
                ProductModel = new ProductModel { ProductId = vendor.vc_productId }
            };

            return await _vendor.UpdateVendorAsync(updatedVendor);
        }

        public async Task<string> DeleteVendorAsync(int id)
        {
            return await _vendor.DeleteVendorAsync(id);
        }

        public async Task<List<ProductModel>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _product.GetProductsByCategoryAsync(categoryId);
        }
    }
}
