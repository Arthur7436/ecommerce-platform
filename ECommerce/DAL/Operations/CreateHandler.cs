using ECommerce.Database;
using ECommerce.GlobalVariables;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.DAL.Operations
{
    public class CreateHandler : Main
    {
        public static void AddToSqlDb(List<Product> ListOfProducts)
        {
            SqlVariables.SetSqlVariables(out var adapter, out var sql, out var cnn);
            command = new SqlCommand(sql, cnn);
            cnn.Open();

            // Assuming the last product in the list is the new one to be added
            var newProduct = ListOfProducts.LastOrDefault();

            if (newProduct != null)
            {
                // Check if the product already exists in the database
                command.CommandText = $"SELECT COUNT(*) FROM dbo.Product WHERE Id = '{newProduct.Id}'";
                int count = (int)command.ExecuteScalar();

                if (count == 0)
                {
                    // Insert the new product as it does not exist in the database
                    command.CommandText = $"Insert into dbo.Product (Identify, Id, NameOfProduct, Description) values({ListOfProducts.Count}, '{newProduct.Id}', '{newProduct.NameOfProduct}', '{newProduct.Description}')";
                    command.ExecuteNonQuery();
                }
            }

            cnn.Close();
        }
    }
}




