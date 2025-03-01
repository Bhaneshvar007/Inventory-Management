using System.Data;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement_System.Services
{
    public class CustomerServices : ICustomer
    {
        private readonly string _connectionString;
        public CustomerServices(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<List<CustomerModel>> GetAllCustomerAsync()
        {
            var customers = new List<CustomerModel>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_GetAllCustomers", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                customers.Add(new CustomerModel
                                {
                                    Cust_id = Convert.ToInt32(reader["Cust_id"]),
                                    Customer_name = reader["Customer_name"].ToString(),
                                    Customer_email = reader["Customer_email"].ToString(),
                                    Customer_address = reader["Customer_address"].ToString(),
                                    Date_of_purchess = Convert.ToDateTime(reader["Date_of_purchess"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Purchess_amount = Convert.ToDecimal(reader["Purchess_amount"]),
                                    Cetegory = new CetegoryModel
                                    {
                                        cetegoryId = Convert.ToInt32(reader["cetegoryId"]),
                                        cetegoryName = reader["cetegoryName"].ToString()
                                    },
                                    Product = new ProductModel
                                    {
                                        ProductId = Convert.ToInt32(reader["ProductId"]),
                                        ProductName = reader["ProductName"].ToString(),
                                        ProductPrice = Convert.ToDecimal(reader["ProductPrice"])
                                    }
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetAllCustomerAsync: " + ex.Message);
            }

            return customers;
        }

        public async Task<string> InsertCustomerAsync(CustomerModel customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_InsertCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Customer_name",customer.Customer_name);
                        cmd.Parameters.AddWithValue("@Customer_email", customer.Customer_email);
                        cmd.Parameters.AddWithValue("@Customer_address", customer.Customer_address);
                        cmd.Parameters.AddWithValue("@Quantity", customer.Quantity);
                        cmd.Parameters.AddWithValue("@Purchess_amount", customer.Purchess_amount);
                         
                        

                        // Handling potential null values
                        cmd.Parameters.AddWithValue("@ProductId", customer?.Product?.ProductId);
                        cmd.Parameters.AddWithValue("@cetegoryId", customer?.Cetegory?.cetegoryId);

                        SqlParameter returnMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(returnMessage);

                        await cmd.ExecuteNonQueryAsync();

                        return returnMessage.Value?.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }


        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetCustomerById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustId", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync()) 
                            {
                                return new CustomerModel()
                                {
                                    Cust_id = reader.GetInt32(reader.GetOrdinal("Cust_id")),
                                    Customer_name = reader.GetString(reader.GetOrdinal("Customer_name")),
                                    Customer_email = reader.GetString(reader.GetOrdinal("Customer_email")),
                                    Customer_address = reader.GetString(reader.GetOrdinal("Customer_address")),
                                    Date_of_purchess = reader.GetDateTime(reader.GetOrdinal("Date_of_purchess")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    Purchess_amount = reader.GetDecimal(reader.GetOrdinal("Purchess_amount")),
                                    Cetegory = new CetegoryModel
                                    {
                                        cetegoryId = reader.GetInt32(reader.GetOrdinal("cetegoryId")),
                                        cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName"))
                                    },
                                    Product = new ProductModel
                                    {
                                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                    },
                                    
                                };

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVendorByIdAsync: {ex.Message}");
                throw;
            }

            return null;  
        }



        public async Task<string> DeleteCustomerAsync(int CustId)
        {
            string returnMessage = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustId", CustId);

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }



        public async Task<string> UpdateCustomerAsync(CustomerModel customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustId", customer.Cust_id);
                        cmd.Parameters.AddWithValue("@Customer_name", customer.Customer_name);
                        cmd.Parameters.AddWithValue("@Customer_email", customer.Customer_email);
                        cmd.Parameters.AddWithValue("@Customer_address", customer.Customer_address);
                        cmd.Parameters.AddWithValue("@Quantity", customer.Quantity);
                        cmd.Parameters.AddWithValue("@Purchess_amount", customer.Purchess_amount);



                        // Handling potential null values
                        cmd.Parameters.AddWithValue("@ProductId", customer?.Product?.ProductId);
                        cmd.Parameters.AddWithValue("@cetegoryId", customer?.Cetegory?.cetegoryId);

                        SqlParameter returnMessage = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(returnMessage);

                        await cmd.ExecuteNonQueryAsync();

                        return returnMessage.Value?.ToString() ?? "Update operation completed with no message.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVendorAsync: {ex.Message}");
                throw;
            }
        }


    }

}
