using System.Data;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement_System.Services
{
    public class ProductSevices : IProduct
    {
        private readonly string _connectionString;
        public ProductSevices(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<ProductModel>> GetProductAsync()
        {
            var products = new List<ProductModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetProducts", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                products.Add(new ProductModel
                                {
                                    ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                    ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    ProductQuantity = reader.GetInt32(reader.GetOrdinal("ProductQuantity")),
                                    ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice")),
                                    ProductBrand = reader.GetString(reader.GetOrdinal("ProductBrand")),
                                    Cetegory = new CetegoryModel
                                    {
                                        cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName"))
                                    }
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return products;
        }

        public async Task<string> CreateProductAsynce(ProductModel product)

        {
            string returnMessage = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_AddProduct", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                        cmd.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                        cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                        cmd.Parameters.AddWithValue("@ProductBrand", product.ProductBrand);
                        cmd.Parameters.AddWithValue("@CetegoryId", product.Cetegory.cetegoryId);

                        SqlParameter outputParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        await cmd.ExecuteNonQueryAsync();
                        returnMessage = outputParam.Value.ToString();
                    }
                }



            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return returnMessage;
        }


        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            ProductModel product = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_GetProductById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", id);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            product = new ProductModel
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                ProductQuantity = Convert.ToInt32(reader["ProductQuantity"]),
                                ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                ProductBrand = reader["ProductBrand"].ToString(),
                                Cetegory = new CetegoryModel
                                {
                                    cetegoryId = reader.GetInt32(reader.GetOrdinal("cetegoryId")),
                                    cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName")),
                                }

                            };
                        }
                    }
                }
            }

            return product;
        }

        public async Task<string> UpdateProductAsync(ProductModel product)
        {
            string returnMessage = "";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@ProductQuantity", product.ProductQuantity);
                    cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
                    cmd.Parameters.AddWithValue("@ProductBrand", product.ProductBrand);
                    cmd.Parameters.AddWithValue("@CetegoryId", product.Cetegory.cetegoryId);

                    SqlParameter outputParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    await cmd.ExecuteNonQueryAsync();
                    returnMessage = outputParam.Value.ToString();
                }
            }

            return returnMessage;
        }

        public async Task<string> DeleteProductAsync(int productId)
        {
            string returnMessage = "";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    SqlParameter outputParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    await cmd.ExecuteNonQueryAsync();
                    returnMessage = outputParam.Value.ToString();
                }
            }

            return returnMessage;
        }

        

        //Get product by cetegory
        public async Task<List<ProductModel>> GetProductsByCategoryAsync(int id)
        {
            var products = new List<ProductModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetProductByCetegory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cetegoryId", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var product = new ProductModel
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                                };

                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

            return products;

        }

        
    }
}
