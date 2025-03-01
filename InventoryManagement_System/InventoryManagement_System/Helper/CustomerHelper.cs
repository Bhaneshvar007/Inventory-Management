using InventoryManagement_System.IHelper;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using InventoryManagement_System.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement_System.Business
{
    public class CustomerHelper : IS_Customer
    {
        private readonly IVendor _vendor;
        private readonly IProduct _product;
        private readonly ICetegory _cetegory;
        private readonly ICustomer _customer;

        public CustomerHelper(ICetegory cetegory, IProduct product, IVendor vendor, ICustomer customer)
        {
            _vendor = vendor;
            _product = product;
            _cetegory = cetegory;
            _customer = customer;
        }

        public async Task<List<CustomerModel>> GetAllCustomers()
        {
            return await _customer.GetAllCustomerAsync();
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            return await _customer.GetCustomerByIdAsync(id);
        }

        public async Task<string> InsertCustomerAsync(VendorCetegoryModel customer)
        {
            var newCustomer = new CustomerModel
            {
                Cust_id = customer.Customers.Cust_id,
                Customer_name = customer.Customers.Customer_name,
                Customer_address = customer.Customers.Customer_address,
                Customer_email = customer.Customers.Customer_email,
                Date_of_purchess = customer.Customers.Date_of_purchess,
                Purchess_amount = customer.Customers.Purchess_amount,
                Quantity = customer.Customers.Quantity,
                Cetegory = new CetegoryModel() { cetegoryId = customer.vc_cetegoryId },
                Product = new ProductModel() { ProductId = customer.vc_productId },
            };


            // Stock Management 
            var products = await _product.GetProductAsync();
            if (products == null)
            {
                return "No products found.";
            }
            var existingProduct = products.FirstOrDefault(p => p.ProductId == newCustomer.Product?.ProductId);

            if (existingProduct != null)
            {
                int updatedQuantity =(int)existingProduct.ProductQuantity;

                if(existingProduct.ProductQuantity > newCustomer.Quantity)
                updatedQuantity = (int)(existingProduct.ProductQuantity - newCustomer.Quantity);
                
                var newProduct = new ProductModel
                {
                    ProductId = existingProduct.ProductId,
                    ProductName = existingProduct.ProductName,
                    ProductPrice = existingProduct.ProductPrice,
                    ProductBrand = existingProduct.ProductBrand,
                    ProductQuantity = updatedQuantity,
                    Cetegory = new CetegoryModel()
                    {
                        cetegoryId = newCustomer.Cetegory.cetegoryId
                    }
                };

                var res = await _product.UpdateProductAsync(newProduct);
            }




            return await _customer.InsertCustomerAsync(newCustomer);
        }

        public async Task<string> UpdateCustomerAsync(VendorCetegoryModel customer)
        {
            var updatedCustomer = new CustomerModel
            {
                Cust_id = customer.Customers.Cust_id,
                Customer_name = customer.Customers.Customer_name,
                Customer_email = customer.Customers.Customer_email,
                Customer_address = customer.Customers.Customer_address,
                Date_of_purchess = customer.Customers.Date_of_purchess,
                Quantity = customer.Customers.Quantity,
                Purchess_amount = customer.Customers.Purchess_amount,
                Cetegory = new CetegoryModel() { cetegoryId = customer.vc_cetegoryId },
                Product = new ProductModel() { ProductId = customer.vc_productId },
            };

            return await _customer.UpdateCustomerAsync(updatedCustomer);
        }

        public async Task<string> DeleteCustomerAsync(int id)
        {
            return await _customer.DeleteCustomerAsync(id);
        }

        public async Task<List<ProductModel>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _product.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<VendorCetegoryModel> GetInsertCustomerDataAsync()
        {
            var categories = await _cetegory.GetCategoryAsync();
            return new VendorCetegoryModel()
            {
                modelType = "Customers",
                V_cetegory = categories,
                Customers = new CustomerModel()
            };
        }

        public async Task<VendorCetegoryModel> GetUpdateCustomerDataAsync(int id)
        {
            var customer = await _customer.GetCustomerByIdAsync(id);
            if (customer == null)
                return null;

            var products = await _product.GetProductAsync();
            var categories = await _cetegory.GetCategoryAsync();

            return new VendorCetegoryModel()
            {
                modelType = "Customers",
                Customers = customer,
                V_cetegory = categories,
                vc_cetegoryId = customer.Cetegory.cetegoryId,
                V_products = products,
                vc_productId = customer.Product?.ProductId ?? 0
            };
        }
    }
}
