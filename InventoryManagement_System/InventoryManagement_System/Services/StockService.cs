using System.Data;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement_System.Services
{
    public class StockService:IStock
    {
        private readonly string _connectionString;
        public StockService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> InsertOrUpdateStockAsync(VendorModel vendor)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertOrUpdateStock", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VendorId", vendor.VendorId);
                        cmd.Parameters.AddWithValue("@Quantity", vendor.Quantity);
                        cmd.Parameters.AddWithValue("@BillingAmount", vendor.Billing_amount);
                        cmd.Parameters.AddWithValue("@ProductId", vendor?.ProductModel?.ProductId);
                        cmd.Parameters.AddWithValue("@cetegoryId", vendor?.CetegoryModel?.cetegoryId);

                        SqlParameter returnMessage = new SqlParameter("@Message", SqlDbType.NVarChar, 255)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(returnMessage);

                        await cmd.ExecuteNonQueryAsync();

                        return returnMessage.Value?.ToString() ?? "No message received";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


    }
}
