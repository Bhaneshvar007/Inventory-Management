using System.Data;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement_System.Services
{
    public class VendorServices : IVendor 
    {
        private readonly string _connectionString;
        public VendorServices(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<List<VendorModel>> GetVendorListAsync()
        {

            var vendors = new List<VendorModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_GetVendors", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                vendors.Add(new VendorModel
                                {
                                    VendorId = reader.GetInt32(reader.GetOrdinal("VendorId")),
                                    VendorName = reader.GetString(reader.GetOrdinal("VendorName")),
                                    VendorEmail = reader.GetString(reader.GetOrdinal("VendorEmail")),
                                    VendorAddress = reader.GetString(reader.GetOrdinal("VendorAddress")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    Billing_amount = reader.GetDecimal(reader.GetOrdinal("Billing_amount")),
                                    Date_of_sale = reader.GetDateTime(reader.GetOrdinal("Date_of_sale")),
                                    CetegoryModel = new CetegoryModel
                                    {
                                        cetegoryId = reader.GetInt32(reader.GetOrdinal("cetegoryId")),
                                        cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName"))

                                    },
                                    ProductModel = new ProductModel
                                    {
                                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                        ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice"))
                                    }
                                });
                            }
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return vendors;
        }

        public async Task<string> InsertVendorAsync(VendorModel vendor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertVendor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VendorName", vendor.VendorName);
                        cmd.Parameters.AddWithValue("@VendorEmail", vendor.VendorEmail);
                        cmd.Parameters.AddWithValue("@VendorAddress", vendor.VendorAddress);
                        cmd.Parameters.AddWithValue("@Date_of_sale", vendor.Date_of_sale);
                        cmd.Parameters.AddWithValue("@Quantity", vendor.Quantity);
                        cmd.Parameters.AddWithValue("@Billing_amount", vendor.Billing_amount);

                        // Handling potential null values
                        cmd.Parameters.AddWithValue("@ProductId", vendor?.ProductModel?.ProductId);
                        cmd.Parameters.AddWithValue("@cetegoryId", vendor?.CetegoryModel?.cetegoryId);

                        SqlParameter returnMessage = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(returnMessage);

                        await cmd.ExecuteNonQueryAsync();

                        return returnMessage.Value?.ToString() ;
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public async Task<VendorModel> GetVendorByIdAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetVendorById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VendorId", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync()) // Ensure data exists before reading
                            {
                                return new VendorModel()
                                {
                                    VendorId = reader.GetInt32(reader.GetOrdinal("VendorId")),
                                    VendorName = reader.GetString(reader.GetOrdinal("VendorName")),
                                    VendorEmail = reader.GetString(reader.GetOrdinal("VendorEmail")),
                                    VendorAddress = reader.GetString(reader.GetOrdinal("VendorAddress")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                    Billing_amount = reader.GetDecimal(reader.GetOrdinal("Billing_amount")),
                                    Date_of_sale = reader.GetDateTime(reader.GetOrdinal("Date_of_sale")),
                                    CetegoryModel = new CetegoryModel
                                    {
                                        cetegoryId = reader.GetInt32(reader.GetOrdinal("cetegoryId")),
                                        cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName"))
                                    },
                                    ProductModel = new ProductModel
                                    {
                                        ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                                        ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                                        ProductPrice = reader.GetDecimal(reader.GetOrdinal("ProductPrice"))
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error instead of just writing to the console
                Console.WriteLine($"Error in GetVendorByIdAsync: {ex.Message}");
                throw;
            }

            return null; // Return null only if no data is found
        }

        public async Task<string> DeleteVendorAsync(int VendorId)
        {
            string returnMessage = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteVendor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VendorId", VendorId);

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

        public async Task<string> UpdateVendorAsync(VendorModel vendor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVendor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VendorId", vendor.VendorId);
                        cmd.Parameters.AddWithValue("@VendorName", vendor.VendorName);
                        cmd.Parameters.AddWithValue("@VendorEmail", vendor.VendorEmail);
                        cmd.Parameters.AddWithValue("@VendorAddress", vendor.VendorAddress);
                        cmd.Parameters.AddWithValue("@Date_of_sale", vendor.Date_of_sale);
                        cmd.Parameters.AddWithValue("@Quantity", vendor.Quantity);
                        cmd.Parameters.AddWithValue("@Billing_amount", vendor.Billing_amount);

                        // Handling potential null values
                        cmd.Parameters.AddWithValue("@ProductId", vendor?.ProductModel?.ProductId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@cetegoryId", vendor?.CetegoryModel?.cetegoryId ?? (object)DBNull.Value);

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
