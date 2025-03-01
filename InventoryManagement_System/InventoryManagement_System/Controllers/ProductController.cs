using System.Reflection;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using InventoryManagement_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProduct product;
        private readonly ICetegory cetegory;

        public ProductController(IProduct product, ICetegory cetegory)
        {
            this.product = product;
            this.cetegory = cetegory;
        }

        [Route("GetAllProduct")]
        public async Task<IActionResult> Index()
        {
            var products = await this.product.GetProductAsync();

            return View(products);
        }



        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await cetegory.GetCategoryAsync();

            if (categories == null || !categories.Any())
            {
                ViewBag.ErrorMessage = "No categories available.";
                return View(new ViewProductModel());
            }

            var viewModel = new ViewProductModel()
            {
                cetegoryModels = categories,
                Product = new ProductModel()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] ViewProductModel viewProductModel)
        {

            //var categorys = await cetegory.GetCategoryByIdAsync(viewProductModel.cetegoryId);

            ModelState.Remove("cetegoryModels");
            ModelState.Remove("Product.Cetegory");

            if (!ModelState.IsValid)
            {
                viewProductModel.cetegoryModels = await cetegory.GetCategoryAsync();
                return View(viewProductModel);
            }



            var newProduct = new ProductModel
            {
                ProductId = viewProductModel.Product.ProductId,
                ProductName = viewProductModel.Product.ProductName,
                ProductBrand = viewProductModel.Product.ProductBrand,
                ProductPrice = viewProductModel.Product.ProductPrice,
                ProductQuantity = viewProductModel.Product.ProductQuantity,
                Cetegory = new CetegoryModel()
                {
                    cetegoryId = viewProductModel.cetegoryId
                }
            };



            var result = await product.CreateProductAsynce(newProduct);
            return RedirectToAction("Index");
        }



        //[HttpPost]
        //[Route("CreateProduct")]
        // public async Task<IActionResult> CreateProduct([FromForm] ViewProductModel viewProductModel)
        //{
        //     //if (!ModelState.IsValid)
        //     //{
        //     //    var categories = await cetegory.GetCategoryAsync();

        //     //    var viewModel = new ViewProductModel()
        //     //    {
        //     //        cetegoryModels = categories,

        //     //    };
        //     //    return View(viewModel);
        //     //}

        //     //ViewBag.message = await this.product.CreateProductAsynce(productModel);

        //     //return RedirectToAction("Index");





        //     var products = new ProductModel
        //     {
        //         ProductName = viewProductModel.Product.ProductName,
        //         ProductBrand = viewProductModel.Product.ProductBrand,
        //         ProductPrice = viewProductModel.Product.ProductPrice,
        //         ProductQuantity = viewProductModel.Product.ProductQuantity,
        //         Cetegory = new CetegoryModel() { cetegoryId = viewProductModel.cetegoryId}
        //     };


        //     if (!TryValidateModel(viewProductModel.Product))
        //     {
        //         viewProductModel.cetegoryModels = await cetegory.GetCategoryAsync();
        //         return View(viewProductModel);
        //     }

        //     var result = await product.CreateProductAsynce(products);
        //     return RedirectToAction("Index");
        // }



        [Route("GetProductById")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            return View(await this.product.GetProductByIdAsync(id));
        }

        //[Route("UpdateProduct")]
        //[HttpGet]
        //public async Task<IActionResult> UpdateProduct(int id)
        //{
        //    var product = await this.product.GetProductByIdAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.Categories = await cetegory.GetCategoryAsync();
        //    return View(product);
        //}

        //[Route("UpdateProduct")]
        //[HttpPost]
        //public async Task<IActionResult> UpdateProduct(ProductModel productModel)
        //{
        //    ViewBag.Message = await product.UpdateProductAsync(productModel);
        //    return RedirectToAction("Index");
        //}



        [Route("UpdateProduct")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            //1.. Product Model Through
            //var product = await _products.GetProductByIdAsync(id);

            //ViewBag.Categories = await _category.GetAllCategoryAsync();
            //return View(product);


            //2.. View Model throw.
            var products = await product.GetProductByIdAsync(id);
            var categories = await cetegory.GetCategoryAsync();

            var viewModel = new ViewProductModel()
            {
                Product = products,
               cetegoryModels = categories,
                cetegoryId = products.Cetegory.cetegoryId
            };
            return View(viewModel);
        }


        [Route("UpdateProduct")]
        [HttpPost]
        //public async Task<IActionResult> Update([FromForm] Product product)
        public async Task<IActionResult> UpdateProduct(ViewProductModel products)
        {
            // 1...............
            //ViewBag.Message = await _products.UpdateProductAsync(product);
            //return RedirectToAction("Index");

            // 2.................
            var updatedProduct = new ProductModel()
            {
                ProductId = products.Product.ProductId,
                ProductName = products.Product.ProductName,
                ProductBrand = products.Product.ProductBrand,
                ProductQuantity = products.Product.ProductQuantity,
                ProductPrice = products.Product.ProductPrice,
                Cetegory = new CetegoryModel()
                {
                    cetegoryId = products.cetegoryId
                }
            };

            ViewBag.Message = await product.UpdateProductAsync(updatedProduct);
            return RedirectToAction("Index");
        }




        [Route("DeleteProduct")]
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ViewBag.Message = await this.product.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }

    }
}
