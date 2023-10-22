using ECommerce.Business_Logic.Operations;
using ECommerce.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Operations
{
    public class UpdateHandler : Update
    {
        public static void UpdateProductDescriptionInSqlDb(string? newProductDescription, int i)
        {
            cnn.Open();
            sql = "Update dbo.Product set Description='" + $"{newProductDescription}" + $"' where Identify={i + 1}"; //Update the column Description at the row of that product

            command = new SqlCommand(sql, cnn);

            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            CloseSqlConnection.CloseSql();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product description updated!");
            Console.ResetColor();
            Thread.Sleep(500);
        }

        public static void UpdateProductNameInSqlDb(string? newProductName, int i)
        {
            cnn.Open();
            sql = "Update dbo.Product set NameOfProduct='" + $"{newProductName}" + $"' where Identify={i + 1}"; //Update the column NameOfProduct at the row of that product

            command = new SqlCommand(sql, cnn);

            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            Console.WriteLine(adapter.UpdateCommand.ExecuteNonQuery());
            adapter.UpdateCommand.ExecuteNonQuery();

            CloseSqlConnection.CloseSql();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product name updated!");
            Console.ResetColor();
            Thread.Sleep(500);
        }
    }
}
