//using InventoryManagement_System.Interface;
//using InventoryManagement_System.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace InventoryManagement_System.Controllers
//{
//    public class CustomerController : Controller
//    {
//        private readonly IVendor _vendor;
//        private readonly IProduct _product;
//        private readonly ICetegory _cetegory;
//        private readonly ICustomer _customer;

//        public CustomerController(ICetegory cetegory,
//            IProduct product, 
//            IVendor vendor, 
//            ICustomer customer)
//        {
//            this._vendor = vendor;
//            this._product = product;
//            this._cetegory = cetegory;
//            this._customer = customer;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var customers = _customer.GetAllCustomerAsync();  
//            return View(customers);
//        }

//        [HttpGet]
//        [Route("InsertCustomer")]
//        public async Task<IActionResult> InsertCustomer()
//        {
//            var cet = await _cetegory.GetCategoryAsync();
//            if (cet != null)
//            {
//                var viewModel = new VendorCetegoryModel()
//                {
//                    V_cetegory =cet,
//                    Customers = new CustomerModel()
//                };

//                return View(viewModel);
//            }

//            ViewBag.ErrorMessage = "No categories available.";
//            return View(new VendorCetegoryModel());
//        }



//        [HttpGet]
//        public async Task<IActionResult> GetProductsByCategory(int categoryId)
//        {
//            var products = await this._product.GetProductsByCategoryAsync(categoryId);

//            return Json(products);

//        }

//        [HttpPost]
//        [Route("InsertCustomer")]
//        public async Task<IActionResult> InsertCustomer([FromForm] VendorCetegoryModel customer)
//        {
//            ModelState.Remove("Customers.Cetegory");
//            ModelState.Remove("Customers.Product");
//            ModelState.Remove("V_products");
//            ModelState.Remove("V_cetegory");
//            ModelState.Remove("vendorModel");

//            if (!ModelState.IsValid)
//            {
//                customer.V_cetegory = await _cetegory.GetCategoryAsync();
//                customer.V_products = await _product.GetProductAsync();

//                return View(customer);
//            }

//            var newCustomer = new CustomerModel
//            {
//                Cust_id = customer.Customers.Cust_id,
//                Customer_name = customer.Customers.Customer_name,
//                Customer_address = customer.Customers.Customer_address,
//                Customer_email = customer.Customers.Customer_email,
//                Date_of_purchess = customer.Customers.Date_of_purchess,
//                Purchess_amount = customer.Customers.Purchess_amount,
//                Quantity = customer.Customers.Quantity,
//                Cetegory = new CetegoryModel() { cetegoryId = customer.vc_cetegoryId },
//                Product = new ProductModel() { ProductId = customer.vc_productId },
//            };

//            string message = await this._customer.InsertCustomerAsync(newCustomer);

//            return RedirectToAction("Index");
//        }

//        [Route("DeleteCustomer")]
//        [HttpGet]
//        public async Task<IActionResult> DeleteCustomer(int id)
//        {
//            ViewBag.Message = await this._customer.DeleteCustomerAsync(id);
//            return RedirectToAction("Index");
//        }



//        [Route("GetCustomerById")]
//        [HttpGet]
//        public async Task<IActionResult> GetCustomerById(int id)
//        {
//            return View(await this._customer.GetCustomerByIdAsync(id));
//        }



//        [Route("UpdateCustomer")]
//        [HttpGet]
//        public async Task<IActionResult> UpdateCustomer(int id)
//        {


//            var customer = await _customer.GetCustomerByIdAsync(id);

//            if (customer == null)
//            {
//                return NotFound();
//            }


//            var products = await _product.GetProductAsync();
//            var cetegories = await _cetegory.GetCategoryAsync();
//            var vendors = await _vendor.GetVendorListAsync();

//            var viewModel = new VendorCetegoryModel()
//            {
//                Customers = customer,
//                V_cetegory = cetegories,
//                vc_cetegoryId = customer.Cetegory.cetegoryId,
//                V_products = products,
//                vc_productId = customer.Product.ProductId ?? 0,

//            };
//            return View(viewModel);
//        }


//        [Route("UpdateCustomer")]
//        [HttpPost]
//        public async Task<IActionResult> UpdateCustomer(VendorCetegoryModel customer)
//        {
//            var newCustomer= new CustomerModel
//            {

//                Cust_id = customer.Customers.Cust_id,
//                Customer_name = customer.Customers.Customer_name,
//                Customer_email = customer.Customers.Customer_email,
//                Customer_address = customer.Customers.Customer_address,
//                Date_of_purchess = customer.Customers.Date_of_purchess,
//                Quantity = customer.Customers.Quantity,
//                Purchess_amount = customer.Customers.Purchess_amount,
//                Cetegory = new CetegoryModel()
//                {
//                    cetegoryId = customer.vc_cetegoryId
//                },
//                Product = new ProductModel()
//                {
//                    ProductId = customer.vc_productId,
//                }

//            };
//            ViewBag.Message = await _customer.UpdateCustomerAsync(newCustomer);
//            return RedirectToAction("Index");
//        }
//    }
//}


using InventoryManagement_System.Business;
using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using InventoryManagement_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagement_System.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IS_Customer _customerService;

        public CustomerController(IS_Customer customerService)
        {
            this._customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomers();
            return View(customers);
        }

        
        [HttpGet]
        [Route("InsertCustomer")]
        public async Task<IActionResult> InsertCustomer()
        {
            var viewModel = await _customerService.GetInsertCustomerDataAsync();
            if (viewModel.V_cetegory == null)
            {
                ViewBag.ErrorMessage = "No categories available.";
            }
            return View(viewModel);
        }

        [HttpPost]
        [Route("InsertCustomer")]
        public async Task<IActionResult> InsertCustomer(VendorCetegoryModel customer)
        {
            ModelState.Remove("Customers.Cetegory");
            ModelState.Remove("Customers.Product");
            ModelState.Remove("V_products");
            ModelState.Remove("V_cetegory");
            ModelState.Remove("vendorModel");
            ModelState.Remove("modelType");


            if (!ModelState.IsValid)
            {
                return View(await _customerService.GetInsertCustomerDataAsync());
            }

            await _customerService.InsertCustomerAsync(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _customerService.GetProductsByCategoryAsync(categoryId);
            return Json(products);
        }

        [Route("DeleteCustomer")]
        [HttpGet]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            ViewBag.Message = await _customerService.DeleteCustomerAsync(id);
            return RedirectToAction("Index");
        }

        [Route("GetCustomerById")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            return View(await _customerService.GetCustomerByIdAsync(id));
        }

        [Route("UpdateCustomer")]
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var viewModel = await _customerService.GetUpdateCustomerDataAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [Route("UpdateCustomer")]
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(VendorCetegoryModel customer)
        {
            await _customerService.UpdateCustomerAsync(customer);
            return RedirectToAction("Index");
        }
    }
}

