using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement_System.Business
{
    public class StockHelper:IS_Stock
    {
        private readonly IVendor _vendor;
        private readonly IProduct _product;
        private readonly ICetegory _cetegory;
        private readonly IStock _stock;

        public StockHelper(IVendor vendor, IProduct product, ICetegory cetegory, IStock stock)
        {
            _vendor = vendor;
            _product = product;
            _cetegory = cetegory;
            _stock = stock;
        }

        public async Task<VendorCetegoryModel> GetVendorCategoryData()
        {
            var categories = await _cetegory.GetCategoryAsync();
            var vendors = await _vendor.GetVendorListAsync();

            return new VendorCetegoryModel
            {
                V_cetegory = categories,
                vendorModel = vendors.FirstOrDefault()
            };
        }

        public async Task<IEnumerable<CetegoryModel>> GetCategoriesByVendor(int vendorId)
        {
            return await _cetegory.GetCetegoryByVendorAsync(vendorId);
        }

        public async Task<string> InsertOrUpdateStock(VendorCetegoryModel vendor)
        {
            var stockData = new VendorModel
            {
                VendorId = vendor.vendorModel.VendorId,
                Quantity = vendor.vendorModel.Quantity,
                Billing_amount = vendor.vendorModel.Billing_amount,
                ProductModel = new ProductModel
                {
                    ProductId = vendor.vc_productId
                },
                CetegoryModel = new CetegoryModel
                {
                    cetegoryId = vendor.vc_cetegoryId
                }
            };

            var result = await _stock.InsertOrUpdateStockAsync(stockData);
            return result ;
        }
    }
}
