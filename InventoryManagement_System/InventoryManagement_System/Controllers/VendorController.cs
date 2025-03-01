//using InventoryManagement_System.IHelper;
//using InventoryManagement_System.Interface;
//using InventoryManagement_System.Models;
//using InventoryManagement_System.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace InventoryManagement_System.Controllers
//{
//    public class VendorController : Controller
//    {
//        private readonly IVendor _vendor;
//        private readonly IProduct _product;
//        private readonly ICetegory _cetegory;


//        public VendorController(IVendor vendor , IProduct product, ICetegory cetegory )
//        {
//            this._vendor = vendor;
//            this._product = product;
//            this._cetegory = cetegory;
//        }


//        [HttpGet]
//        [Route("GetVendor")]

//        public async Task<IActionResult> Index()
//        {
//            var vendors = await this._vendor.GetVendorListAsync();
//            return View(vendors);
//        }

//        [HttpGet]
//        [Route("InsertVendor")]
//        public async Task<IActionResult> InsertVendor()
//        {
//            //var categories = await _cetegory.GetCategoryAsync();
//            //var products = await _product.GetProductAsync();

//            //ViewBag.Categories = categories ?? new List<CetegoryModel>();
//            //ViewBag.Products = products ?? new List<ProductModel>();

//            //return View();


//            var cetegories = await _cetegory.GetCategoryAsync();
//            var products = await _product.GetProductAsync();
//            if (cetegories.Count > 0 && products.Count > 0)
//            {
//                var viewModel = new VendorViewModel()
//                {
//                    V_cetegory = cetegories,
//                    V_products =null,
//                };

//                return View(viewModel);
//            }
//            ViewBag.ErrorMessage = "No categories available.";
//            return View(new VendorViewModel());
//        }

//        [HttpPost]
//        [Route("InsertVendor")]
//        public async Task<IActionResult> InsertVendor([FromForm]VendorViewModel vendor)
//        {
//            ////if (!ModelState.IsValid)
//            ////{
//            ////    var categories = await cetegory.GetCategoryAsync();
//            ////    var products = await product.GetProductAsync();

//            ////    ViewBag.Categories = categories ?? new List<CetegoryModel>();
//            ////    ViewBag.Products = products ?? new List<ProductModel>();

//            ////    return View(vendor);
//            ////}

//            //string message = await this._vendor.InsertVendorAsync(vendor);
//            //ViewBag.Message = message;
//            //return RedirectToAction("Index");


//            ModelState.Remove("vendorModel.CetegoryModel");
//            ModelState.Remove("vendorModel.ProductModel");
//            ModelState.Remove("V_products");
//            ModelState.Remove("V_cetegory");


//            if (!ModelState.IsValid)
//            {
//                vendor.V_cetegory = await _cetegory.GetCategoryAsync();
//                vendor.V_products = await _product.GetProductAsync();

//                return View(vendor);
//            }


//            var newVendor = new VendorModel
//            {

//                VendorId = vendor.vendorModel.VendorId,
//                VendorName = vendor.vendorModel.VendorName,
//                VendorAddress = vendor.vendorModel.VendorAddress,   
//                VendorEmail = vendor.vendorModel.VendorEmail,   
//                Billing_amount = vendor.vendorModel.Billing_amount,
//                Date_of_sale = vendor.vendorModel.Date_of_sale,
//                Quantity = vendor.vendorModel.Quantity,
//                CetegoryModel = new CetegoryModel()
//                {
//                    cetegoryId = vendor.v_cetegoryId
//                },
//                ProductModel = new ProductModel()
//                {
//                    ProductId = vendor.v_productId  
//                }
//            };
//            string message = await this._vendor.InsertVendorAsync(newVendor);

//            return RedirectToAction("Index");
//        }





//            //[HttpGet]
//            //public async Task<IActionResult> GetProductsByCategory(int categoryId)
//            //{
//            ////var products = await _product.GetProductAsync();

//            ////var filteredProducts = products.Where(x => x.Cetegory.cetegoryId == categoryId)
//            ////.Select(p => new {p.ProductId , p.ProductName}).ToList();

//            ////return Json(filteredProducts);

//            //var newProduct = await this._product.GetProductsByCategoryAsync(categoryId);
//            //ViewBag.products = newProduct;
//            //return View();
//            //}


//        [HttpGet]
//        public async Task<IActionResult> GetProductsByCategory(int categoryId)
//        {
//            var products = await this._product.GetProductsByCategoryAsync(categoryId);
//            return Json(products);
//        }


//        [Route("GetVendorById")]
//        [HttpGet]
//        public async Task<IActionResult> GetVendorById(int id)
//        {
//            return View(await this._vendor.GetVendorByIdAsync(id));
//        }





//        [Route("UpdateVendor")]
//        [HttpGet]
//        public async Task<IActionResult> UpdateVendor(int id)
//        {
//            var vendors = await _vendor.GetVendorByIdAsync(id);

//            if(vendors == null)
//            {
//                return NotFound();
//            }


//            var products = await _product.GetProductAsync();
//            var cetegories = await _cetegory.GetCategoryAsync();

//            var viewModel = new VendorViewModel()
//            {
//                vendorModel = vendors,
//                V_cetegory = cetegories,
//                v_cetegoryId = vendors.CetegoryModel.cetegoryId,
//                V_products = products,
//                v_productId = vendors.ProductModel.ProductId ?? 0
//            };
//            return View(viewModel);
//        }


//        [Route("UpdateVendor")]
//        [HttpPost]
//        public async Task<IActionResult> UpdateVendor(VendorViewModel vendor)
//        {
//            var newVendor = new VendorModel
//            {

//                VendorId = vendor.vendorModel.VendorId,
//                VendorName = vendor.vendorModel.VendorName,
//                VendorAddress = vendor.vendorModel.VendorAddress,
//                VendorEmail = vendor.vendorModel.VendorEmail,
//                Billing_amount = vendor.vendorModel.Billing_amount,
//                Date_of_sale = vendor.vendorModel.Date_of_sale,
//                Quantity = vendor.vendorModel.Quantity,
//                CetegoryModel = new CetegoryModel()
//                {
//                    cetegoryId = vendor.v_cetegoryId
//                },
//                ProductModel = new ProductModel()
//                {
//                    ProductId = vendor.v_productId
//                }
//            };
//            ViewBag.Message = await _vendor.UpdateVendorAsync(newVendor);
//            return RedirectToAction("Index");
//        }



//        [Route("DeleteVendor")]
//        [HttpGet]
//        public async Task<IActionResult> DeleteVendor(int id)
//        {
//            ViewBag.Message = await this._vendor.DeleteVendorAsync(id);
//            return RedirectToAction("Index");
//        }


//    }
//}



using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagement_System.Controllers
{
    public class VendorController : Controller
    {
        private readonly IS_Vendor _vendorHelper;

        public VendorController(IS_Vendor vendorHelper)
        {
            _vendorHelper = vendorHelper;
        }

        [HttpGet]
        [Route("GetVendor")]
        public async Task<IActionResult> Index()
        {
            var vendors = await _vendorHelper.GetVendorListAsync();
            return View(vendors);
        }

        [HttpGet]
        [Route("InsertVendor")]
        public async Task<IActionResult> InsertVendor()
        {
            var res = await _vendorHelper.InsertVendor();

            if(res == null)
                 return View(new VendorCetegoryModel());

            return View(res);
        }



        [HttpPost]
        [Route("InsertVendor")]
        public async Task<IActionResult> InsertVendor(VendorCetegoryModel vendor)
        {

            ModelState.Remove("vendorModel.CetegoryModel");
            ModelState.Remove("vendorModel.ProductModel");
            ModelState.Remove("V_products");
            ModelState.Remove("V_cetegory");
            ModelState.Remove("Customers");
            ModelState.Remove("modelType");


            if (!ModelState.IsValid)
            {
                var res = await _vendorHelper.InsertVendor();
                return View(res);
            }

            await _vendorHelper.InsertVendorAsync(vendor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("GetVendorById")]
        public async Task<IActionResult> GetVendorById(int id)
        {
            var vendor = await _vendorHelper.GetVendorByIdAsync(id);
            if (vendor == null) return NotFound();

            return View(vendor);
        }

        [HttpGet]
        [Route("UpdateVendor")]
        public async Task<IActionResult> UpdateVendor(int id)
        {
            var vendor = await _vendorHelper.GetVendorByIdAsync(id);
            if (vendor == null) return NotFound();

            return View(vendor);
        }

        [HttpPost]
        [Route("UpdateVendor")]
        public async Task<IActionResult> UpdateVendor(VendorCetegoryModel vendor)
        {
            

            await _vendorHelper.UpdateVendorAsync(vendor);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("DeleteVendor")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            await _vendorHelper.DeleteVendorAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
         
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _vendorHelper.GetProductsByCategoryAsync(categoryId);
            return Json(products);
        }
    }
}
