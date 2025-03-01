using System.Data;
using InventoryManagement_System.Interface;
using InventoryManagement_System.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagement_System.Services
{
    public class AuthService : IAuth
    {
        private readonly string _connectionString;
        public AuthService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Auth>> GetAuthUserAsync()
        {
            var user = new List<Auth>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_getUsers", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                user.Add(new Auth
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    Password = reader.GetString(reader.GetOrdinal("Password")),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching user from the database." + ex);
                throw;
            }

            return user;
        }
        public async Task<string> RegistrationAsync(Auth auth)
        {
            string resultMessage = "Insertion failed";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("sp_register", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Username", auth.Username);
                        cmd.Parameters.AddWithValue("@Email", auth.Email);
                        cmd.Parameters.AddWithValue("@Password", auth.Password);

                        SqlParameter outputParam = new SqlParameter
                        {
                            ParameterName = "@returnMessage",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 255,
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        await cmd.ExecuteNonQueryAsync();

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
    }
}
