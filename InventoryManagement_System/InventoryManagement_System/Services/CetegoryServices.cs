using System.Data;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement_System.Services
{
    public class CetegoryServices:ICetegory
    {
        private readonly string _connectionString;
        public CetegoryServices(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<CetegoryModel>> GetCategoryAsync()
        {
            var categories = new List<CetegoryModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetCetegories", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                categories.Add(new CetegoryModel
                                {
                                    cetegoryId = reader.GetInt32(reader.GetOrdinal("cetegoryId")),
                                    cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine( "Error fetching categories from the database." + ex);
                throw;
            }

            return categories;
        }



        public async Task<CetegoryModel> GetCategoryByIdAsync(int id)
        {
            CetegoryModel category = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetCetegoryById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cetegoryId", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                category = new CetegoryModel
                                {
                                    cetegoryId = reader.GetInt32(reader.GetOrdinal("cetegoryId")),
                                    cetegoryName = reader.GetString(reader.GetOrdinal("cetegoryName"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching category by ID from the database." + ex);
                throw;
            }

            return category;
        }

        public async Task<string> InsertCetegoryAsync(string cetegoryName)
        {
            string resultMessage = "Insertion failed";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertCetegory", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Input Parameter
                        cmd.Parameters.AddWithValue("@cetegoryName", cetegoryName);

                        // Output Parameter
                        SqlParameter outputParam = new SqlParameter
                        {
                            ParameterName = "@returnMessage",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 255,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        // Execute command
                        await cmd.ExecuteNonQueryAsync();

                        // Retrieve the output parameter value
                        resultMessage = outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                resultMessage = $"Error inserting category: {ex.Message}";
            }

            return resultMessage;
        }

        public async Task<string> UpdateCetegoryAsync(CetegoryModel cetegory)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_UpdateCategory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cetegoryId", cetegory.cetegoryId);

                        cmd.Parameters.AddWithValue("@cetegoryName", cetegory.cetegoryName);
                        
                        SqlParameter resultMessageParam = new SqlParameter("@ReturnMessage",
                            SqlDbType.NVarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(resultMessageParam);

                        await cmd.ExecuteNonQueryAsync();

                        return resultMessageParam.Value?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error updating Cetegory: " + ex.Message;
            }
            return null;
        }


        public async Task<string> DeleteCetegoryAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteCategory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cetegoryId", id);

                        SqlParameter outputParam = new SqlParameter("@returnMessage",
                            SqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        await cmd.ExecuteNonQueryAsync();

                        return outputParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

        }

        public async Task<List<CetegoryModel>> GetCetegoryByVendorAsync(int id)
        {

            var cetegorys = new List<CetegoryModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_GetCategoriesByVendor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VendorId", id);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var  cetegory = new CetegoryModel
                                {
                                    cetegoryId = Convert.ToInt32(reader["cetegoryId"]),
                                    cetegoryName = reader["cetegoryName"].ToString(),
                                };

                                cetegorys.Add(cetegory);
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

            return cetegorys;

        }

    }

}
