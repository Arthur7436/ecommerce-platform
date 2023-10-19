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

            SqlVariables.SetSqlVariables(out adapter, out sql, out cnn);

            //access the last product in list
            var product = ListOfProducts.LastOrDefault();


            //Loop through the list and find the row number + Id + NameOfProduct + Description in order to be sent to sql db
            for (int i = 0; i < ListOfProducts.Count; i++)
            {
                sql = $"Insert into dbo.Product (Identify,Id,NameOfProduct,Description) values({i + 1},'" + $"{product.Id}" + "', '" + $"{product.NameOfProduct}" + "' , '" + $"{product.Description}" + "')";
            }

            //push the product into sql db
            command = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);
            cnn.Open();
            adapter.InsertCommand.ExecuteNonQuery();

            CloseSqlConnection.CloseSql();

            Console.ReadLine();
        }
    }
}
