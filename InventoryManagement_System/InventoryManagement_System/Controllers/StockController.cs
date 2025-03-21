﻿using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using InventoryManagement_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagement_System.Controllers
{
    public class StockController : Controller
    {
        private readonly IS_Stock _stockService;
        private readonly IVendor _vendor;

        public StockController(IS_Stock  stockService , IVendor vendor)
        {
            _stockService = stockService;
            this._vendor = vendor;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _stockService.GetVendorCategoryData();

            ViewBag.VendorList = await _vendor.GetVendorListAsync();  
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesByVendor(int vendorId)
        {
            var res = await _stockService.GetCategoriesByVendor(vendorId);
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> InsertStock(VendorCetegoryModel vendor)
        {
            string success = await _stockService.InsertOrUpdateStock(vendor);
            
            return RedirectToAction("Index", "Vendor");
        }
    }
}




// preview code without bisiness layer

//using InventoryManagement_System.Interface;
//using InventoryManagement_System.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace InventoryManagement_System.Controllers
//{
//    public class StockController : Controller
//    {
//        private readonly IVendor _vendor;
//        private readonly IProduct _product;
//        private readonly ICetegory _cetegory;
//        private readonly IStock _stock;

//        public StockController(IVendor vendor,
//            IProduct product,
//            ICetegory cetegory,
//            IStock stock
//            )
//        {
//            this._vendor = vendor;
//            this._product = product;
//            this._cetegory = cetegory;
//            this._stock = stock;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var categories = await _cetegory.GetCategoryAsync();
//            var vendors = await _vendor.GetVendorListAsync();

//            var viewModel = new VendorCetegoryModel
//            {
//                V_cetegory = categories,
//                vendorModel = vendors.FirstOrDefault() 
//            };

//            ViewBag.VendorList = vendors; 
//            return View(viewModel);
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetCategoriesByVendor(int vendorId)
//        {
//            var res = await this._cetegory.GetCetegoryByVendorAsync(vendorId);
//            return Json(res);
//        }



//        [HttpPost]
//        public async Task<IActionResult> InsertStock(VendorCetegoryModel vendor)
//        {
//            var viewModel = new VendorModel()
//            {
//                VendorId = vendor.vendorModel.VendorId,
//                Quantity = vendor.vendorModel.Quantity,
//                Billing_amount = vendor.vendorModel.Billing_amount,
//                ProductModel = new ProductModel()
//                {
//                    ProductId = vendor.vc_productId,
//                },
//                CetegoryModel = new CetegoryModel()
//                {
//                    cetegoryId = vendor.vc_cetegoryId,
//                }
//            };

//            await _stock.InsertOrUpdateStockAsync(
//               viewModel
//            );

//            ViewBag.Message = "Stock updated successfully!";
//            return RedirectToAction("Index", "Vendor");





//            //var products = await _product.GetProductAsync();
//            //if (products == null || !products.Any())
//            //{
//            //    ModelState.AddModelError("", "No products found.");
//            //    return View(vendor);
//            //}

//            //var existingProduct = products.FirstOrDefault(p => p.ProductId == vendor.vc_productId);
//            //if (existingProduct != null)
//            //{
//            //    var existingVendorStock = await _vendor.GetVendorListAsync();
//            //    var vendorStock = existingVendorStock.FirstOrDefault(v =>
//            //        v.VendorId == vendor.vendorModel.VendorId);

//            //    if (vendorStock != null)
//            //    {
//            //        existingProduct.Cetegory.cetegoryId = vendor.vc_cetegoryId;
//            //        existingProduct.ProductQuantity += vendor.vendorModel.Quantity;
//            //        await _product.UpdateProductAsync(existingProduct);
//            //    }
//            //}

//            //var allVendors = await _vendor.GetVendorListAsync();
//            //var vend = await _vendor.GetVendorByIdAsync(vendor.vendorModel.VendorId);
//            //var existingVendor = allVendors.FirstOrDefault(v =>
//            //    v.VendorId == vendor.vendorModel.VendorId && v.ProductModel.ProductId == vendor.vc_productId);

//            //if (existingVendor != null)
//            //{
//            //    var updatedVendor = new VendorModel
//            //    {
//            //        VendorId = existingVendor.VendorId,
//            //        VendorName = existingVendor.VendorName,
//            //        VendorAddress = existingVendor.VendorAddress,
//            //        VendorEmail = existingVendor.VendorEmail,
//            //        Billing_amount = vendor.vendorModel.Billing_amount,
//            //        Date_of_sale = existingVendor.Date_of_sale,
//            //        Quantity = vendor.vendorModel.Quantity,
//            //        CetegoryModel = new CetegoryModel { cetegoryId = vendor.vc_cetegoryId },
//            //        ProductModel = new ProductModel { ProductId = vendor.vc_productId }
//            //    };
//            //    await _vendor.UpdateVendorAsync(updatedVendor);
//            //}
//            //else
//            //{
//            //    var newVendor = new VendorModel
//            //    {
//            //        VendorName = vend.VendorName,
//            //        VendorAddress = vend.VendorAddress,
//            //        VendorEmail = vend.VendorEmail,
//            //        Billing_amount = vendor.vendorModel.Billing_amount,
//            //        Date_of_sale = vend.Date_of_sale,
//            //        Quantity = vendor.vendorModel.Quantity,
//            //        CetegoryModel = new CetegoryModel { cetegoryId = vendor.vc_cetegoryId },
//            //        ProductModel = new ProductModel { ProductId = vendor.vc_productId }
//            //    };
//            //    await _vendor.InsertVendorAsync(newVendor);
//            //    return RedirectToAction("Index", "Vendor");

//            //}

//            //ViewBag.Message = "Stock updated successfully!";
//            //return RedirectToAction("Index", "Product");



//        }



//    }
//}






