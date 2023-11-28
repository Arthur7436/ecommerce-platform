using ECommerce.Business_Logic.Operations;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace ECommerce.DAL.Operations
{
    public class UpdateHandler : Update
    {
        private static string GetConnectionString()
        {
            string username = File.ReadAllText(@"C:\ProjectSecrets\username.txt");
            string dataSource = File.ReadAllText(@"C:\ProjectSecrets\dataSourceName.txt");
            string password = File.ReadAllText(@"C:\ProjectSecrets\EcommerceSecrets.txt");
            string dataBase = "dbProduct";

            return $"Data Source={dataSource};Initial Catalog={dataBase};User ID={username};Password={password}";
        }

        public static void UpdateProductDescriptionInSqlDb(string newProductDescription, int i)
        {
            string connectionString = GetConnectionString();
            string sql = "Update dbo.Product set Description = @desc where Identify = @id";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    command.Parameters.AddWithValue("@desc", newProductDescription);
                    command.Parameters.AddWithValue("@id", i + 1);

                    cnn.Open();
                    command.ExecuteNonQuery();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product description updated!");
            Console.ResetColor();
            Thread.Sleep(500);
        }

        public static void UpdateProductNameInSqlDb(string newProductName, int i)
        {
            string connectionString = GetConnectionString();
            string sql = "Update dbo.Product set NameOfProduct = @name where Identify = @id";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(sql, cnn))
                {
                    command.Parameters.AddWithValue("@name", newProductName);
                    command.Parameters.AddWithValue("@id", i + 1);

                    cnn.Open();
                    Console.WriteLine(command.ExecuteNonQuery());
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product name updated!");
            Console.ResetColor();
            Thread.Sleep(500);
        }
    }
}



